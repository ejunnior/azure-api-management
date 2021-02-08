namespace Finance.Infrastructure.Data.UnitOfWork
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using Domain.Category.Aggregates.CategoryAggregate;
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using FluentNHibernate.Conventions.Helpers;
    using NHibernate;
    using NHibernate.Tool.hbm2ddl;

    public class FinanceUnitOfWork : IFinanceUnitOfWork
    {
        private static readonly ISessionFactory SessionFactory;
        private readonly ISession _session;

        private IQueryable<BankAccount> _bankAccount;
        private IQueryable<BankPosting> _bankPosting;
        private IQueryable<Category> _category;
        private IQueryable<Creditor> _creditor;
        private ITransaction _transaction;

        static FinanceUnitOfWork()
        {
            SessionFactory = BuildSessionFactory();
        }

        public FinanceUnitOfWork()
        {
            _session = SessionFactory.OpenSession();
        }

        public IQueryable<BankAccount> BankAccount => _bankAccount ??= _session.Query<BankAccount>();

        public IQueryable<BankPosting> BankPosting => _bankPosting ??= _session.Query<BankPosting>();

        public IQueryable<Category> Category => _category ??= _session.Query<Category>();

        public IQueryable<Creditor> Creditor => _creditor ??= _session.Query<Creditor>();

        private static string ConnectionString => Environment.GetEnvironmentVariable("ConnectionStrings__FinanceConnectionString");

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    await _transaction.CommitAsync();
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                    await _transaction.RollbackAsync();

                throw;
            }
            finally
            {
                _session.Dispose();
            }
        }

        public ISession CreateSet()
        {
            return _session;
        }

        public async Task RollbackChangesAsync()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    await _transaction.RollbackAsync();
            }
            finally
            {
                _session.Dispose();
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(ConnectionString)
                    .ShowSql())
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(DefaultLazy.Never()))
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                    .Create(false, false))
                .BuildSessionFactory();
        }
    }
}
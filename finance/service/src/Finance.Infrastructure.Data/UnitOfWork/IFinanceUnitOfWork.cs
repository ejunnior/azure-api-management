namespace Finance.Infrastructure.Data.UnitOfWork
{
    using System.Linq;
    using Core;
    using Domain.Bank.Aggregates.BankAccountAggregate;
    using Domain.Category.Aggregates.CategoryAggregate;
    using Domain.Creditor.Aggregates.CreditorAggregate;
    using Domain.Treasury.Aggregates.BankPostingAggregate;

    public interface IFinanceUnitOfWork : IQueryableUnitOfWork
    {
        IQueryable<BankAccount> BankAccount { get; }

        IQueryable<BankPosting> BankPosting { get; }

        IQueryable<Category> Category { get; }

        IQueryable<Creditor> Creditor { get; }
    }
}
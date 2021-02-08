namespace Finance.Infrastructure.Data.Core
{
    using Domain.Core;
    using NHibernate;

    public interface IQueryableUnitOfWork
          : IUnitOfWork
    {
        ISession CreateSet();
    }
}
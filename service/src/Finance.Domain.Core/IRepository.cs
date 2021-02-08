namespace Finance.Domain.Core
{
    using System;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
           where TEntity : AggregateRoot
    {
        Task AddAsync(TEntity item);

        Task<TEntity> GetAsync(Guid id);

        Task ModifyAsync(TEntity item);

        Task RemoveAsync(TEntity item);
    }
}
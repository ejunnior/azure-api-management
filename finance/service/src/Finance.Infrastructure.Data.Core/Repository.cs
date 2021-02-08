namespace Finance.Infrastructure.Data.Core
{
    using Domain.Core;
    using NHibernate;
    using System;
    using System.Threading.Tasks;

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : AggregateRoot
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                          throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task AddAsync(TEntity item)
        {
            if (item != null)
                await GetSet()
                    .SaveAsync(item);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            if (id != default)
                return
                    await GetSet()
                        .GetAsync<TEntity>(id);
            return null;
        }

        public async Task ModifyAsync(TEntity item)
        {
            if (item != null)
                await GetSet()
                    .UpdateAsync(item);
        }

        public async Task RemoveAsync(TEntity item)
        {
            if (item != null)
                await GetSet()
                    .DeleteAsync(item);
        }

        private ISession GetSet()
        {
            return _unitOfWork.CreateSet();
        }
    }
}
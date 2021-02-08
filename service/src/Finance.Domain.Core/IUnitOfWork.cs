namespace Finance.Domain.Core
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        void BeginTransaction();

        Task CommitAsync();

        Task RollbackChangesAsync();
    }
}
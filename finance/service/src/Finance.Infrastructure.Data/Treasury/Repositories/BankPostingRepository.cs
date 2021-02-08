namespace Finance.Infrastructure.Data.Treasury.Repositories
{
    using Core;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using UnitOfWork;

    public class BankPostingRepository
        : Repository<BankPosting>, IBankPostingRepository
    {
        private readonly IFinanceUnitOfWork _unitOfWork;

        public BankPostingRepository(IFinanceUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
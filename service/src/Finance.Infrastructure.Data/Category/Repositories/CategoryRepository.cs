namespace Finance.Infrastructure.Data.Category.Repositories
{
    using Core;
    using Domain.Category.Aggregates.CategoryAggregate;
    using UnitOfWork;

    public class CategoryRepository
        : Repository<Category>, ICategoryRepository
    {
        private readonly IFinanceUnitOfWork _unitOfWork;

        public CategoryRepository(IFinanceUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
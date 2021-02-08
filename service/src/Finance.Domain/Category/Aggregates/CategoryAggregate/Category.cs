namespace Finance.Domain.Category.Aggregates.CategoryAggregate
{
    using Core;

    public class Category : AggregateRoot
    {
        public Category(
            CategoryName name,
            CategoryType type)
        {
            Name = name;
            Type = type;
        }

        private Category()
        {
        }

        public CategoryName Name { get; }

        public CategoryType Type { get; }
    }
}
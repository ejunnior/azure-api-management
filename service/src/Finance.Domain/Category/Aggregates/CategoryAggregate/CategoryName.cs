namespace Finance.Domain.Category.Aggregates.CategoryAggregate
{
    using CSharpFunctionalExtensions;

    public class CategoryName : Core.ValueObject<CategoryName>
    {
        private CategoryName(string value)
        {
            Value = value;
        }

        private CategoryName()
        {
        }

        public string Value { get; }

        public static Result<CategoryName> Create(string value)
        {
            return Result.Success(new CategoryName(value));
        }

        public static explicit operator CategoryName(string value)
        {
            return Create(value).Value;
        }

        public static implicit operator string(CategoryName name)
        {
            return name.Value;
        }
    }
}
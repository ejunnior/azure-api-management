namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using CSharpFunctionalExtensions;

    public class DocumentNumber : Core.ValueObject<DocumentNumber>
    {
        private DocumentNumber(string value)
        {
            Value = value;
        }

        private DocumentNumber()
        {
        }

        public string Value { get; }

        public static Result<DocumentNumber> Create(string value)
        {
            return Result.Success(new DocumentNumber(value));
        }

        public static explicit operator DocumentNumber(string value)
        {
            return Create(value).Value;
        }

        public static implicit operator string(DocumentNumber number)
        {
            return number.Value;
        }
    }
}
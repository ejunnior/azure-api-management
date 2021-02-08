namespace Finance.Domain.Creditor.Aggregates.CreditorAggregate
{
    using CSharpFunctionalExtensions;

    public class CreditorName : Core.ValueObject<CreditorName>
    {
        private CreditorName(string value)
        {
            Value = value;
        }

        private CreditorName()
        {
        }

        public string Value { get; }

        public static Result<CreditorName> Create(string value)
        {
            return Result.Success(new CreditorName(value));
        }

        public static explicit operator CreditorName(string value)
        {
            return Create(value).Value;
        }

        public static implicit operator string(CreditorName name)
        {
            return name.Value;
        }
    }
}
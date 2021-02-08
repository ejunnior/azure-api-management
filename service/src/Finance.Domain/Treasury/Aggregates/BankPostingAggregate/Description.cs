namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using CSharpFunctionalExtensions;

    public class Description : Core.ValueObject<Description>
    {
        private Description(string value)
        {
            Value = value;
        }

        private Description()
        {
        }

        public string Value { get; }

        public static Result<Description> Create(Maybe<string> descriptionOrNothing)
        {
            return descriptionOrNothing
                .ToResult(errorMessage: Errors.General.ValueIsRequired())
                .Tap(description => description.Trim())
                .Ensure(description => description != string.Empty, Errors.General.ValueIsRequired())
                .Ensure(description => description.Length <= 80, Errors.General.ValueIsTooLong())
                .Map(description => new Description(description));
        }

        public static explicit operator Description(string value)
        {
            return Create(value).Value;
        }

        public static implicit operator string(Description number)
        {
            return number.Value;
        }
    }
}
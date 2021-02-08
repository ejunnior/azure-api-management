namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using Core;
    using CSharpFunctionalExtensions;

    public class Amount
    {
        private Amount()
        {
        }

        private Amount(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }

        public static Result<Amount> Create(decimal value)
        {
            if (value <= 0)
                return Result.Failure<Amount>(
                    Errors.General.ValueIsInvalid());

            return Result.Success<Amount>(new Amount(value));
        }

        public static explicit operator Amount(decimal totalAmount)
        {
            return Create(totalAmount).Value;
        }

        public static implicit operator decimal(Amount totalAmount)
        {
            return totalAmount.Value;
        }
    }
}
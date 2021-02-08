namespace Finance.Domain.Creditor.Aggregates.CreditorAggregate
{
    using CSharpFunctionalExtensions;

    public class MobilePhone : Core.ValueObject<MobilePhone>
    {
        private MobilePhone(string number)
        {
            Number = number;
        }

        private MobilePhone()
        {
        }

        public string Number { get; }

        public static Result<MobilePhone> Create(Maybe<string> numberOrNothing)
        {
            return numberOrNothing.ToResult("Mobile phone should not be empty")
                    .OnSuccess(mobilePhone => mobilePhone.Trim())
                    .Ensure(mobilePhone => mobilePhone != string.Empty, "Mobile phone should not be empty")
                    .Ensure(mobilePhone => mobilePhone.Length <= 9, "Mobile phone is too long")
                    .Map(mobilePhone => new MobilePhone(mobilePhone));
        }

        public static explicit operator MobilePhone(string number)
        {
            return Create(number).Value;
        }

        public static implicit operator string(MobilePhone number)
        {
            return number.Number;
        }
    }
}
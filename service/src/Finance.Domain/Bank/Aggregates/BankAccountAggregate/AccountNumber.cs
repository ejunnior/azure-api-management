namespace Finance.Domain.Bank.Aggregates.BankAccountAggregate
{
    using CSharpFunctionalExtensions;

    public class AccountNumber : Core.ValueObject<AccountNumber>
    {
        private AccountNumber(
            string value)
        {
            Value = value;
        }

        private AccountNumber()
        {
        }

        public string Value { get; }

        public static Result<AccountNumber> Create(string value)
        {
            return Result.Success(new AccountNumber(value));
        }

        public static explicit operator AccountNumber(string value)
        {
            return Create(value).Value;
        }

        public static implicit operator string(AccountNumber accountNumber)
        {
            return accountNumber.Value;
        }
    }
}
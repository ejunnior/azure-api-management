namespace Finance.Domain.Bank.Aggregates.BankAccountAggregate
{
    using Core;

    public class BankAccount : AggregateRoot
    {
        public BankAccount(
            AccountNumber accountNumber,
            Bank bank)
        {
            Bank = bank;
        }

        private BankAccount()
        {
        }

        public AccountNumber AccountNumber { get; }

        public Bank Bank { get; }
    }
}
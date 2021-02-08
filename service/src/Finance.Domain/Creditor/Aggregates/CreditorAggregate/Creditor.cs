namespace Finance.Domain.Creditor.Aggregates.CreditorAggregate
{
    using Core;
    using CSharpFunctionalExtensions;

    public class Creditor : AggregateRoot
    {
        private string _email;

        private string _mobilePhone;

        public Creditor(
            CreditorName name,
            Maybe<MobilePhone> mobilePhone,
            Maybe<Email> email)
        {
            Name = name;
            MobilePhone = mobilePhone;
            Email = email;
        }

        private Creditor()
        {
        }

        public Maybe<Email> Email
        {
            get => _email == null ? null : (Email)_email;
            private set => _email = value.Unwrap(email => email.Value);
        }

        public Maybe<MobilePhone> MobilePhone
        {
            get => _mobilePhone == null ? null : (MobilePhone)_mobilePhone;
            private set => _mobilePhone = value.Unwrap(phone => phone.Number);
        }

        public CreditorName Name { get; }
    }
}
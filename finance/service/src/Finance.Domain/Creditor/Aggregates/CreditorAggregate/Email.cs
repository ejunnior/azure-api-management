namespace Finance.Domain.Creditor.Aggregates.CreditorAggregate
{
    using System.Text.RegularExpressions;
    using CSharpFunctionalExtensions;

    public class Email : Core.ValueObject<Email>
    {
        private Email(string value)
        {
            Value = value;
        }

        private Email()
        {
        }

        public string Value { get; }

        public static Result<Email> Create(Maybe<string> emailOrNothing)
        {
            return emailOrNothing.ToResult("Email should not be empty")
                .OnSuccess(email => email.Trim())
                .Ensure(email => email != string.Empty, "Email should not be empty")
                .Ensure(email => email.Length <= 255, "Email is too long")
                .Ensure(email => Regex.IsMatch(email, @"^(.+)@(.+)$"), "Email is invalid")
                .Map(email => new Email(email));
        }

        public static explicit operator Email(string value)
        {
            return Create(value).Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}
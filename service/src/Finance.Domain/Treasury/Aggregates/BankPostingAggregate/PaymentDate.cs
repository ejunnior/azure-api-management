namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using System;
    using Core;
    using CSharpFunctionalExtensions;

    public class PaymentDate
    {
        private PaymentDate()
        {
        }

        private PaymentDate(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }

        public static Result<PaymentDate> Create(Maybe<DateTime> paymentDateOrNothing)
        {
            return paymentDateOrNothing.ToResult("Payment data should not be empty")
                .Map(paymentDate => new PaymentDate(paymentDate));
        }

        public static explicit operator PaymentDate(DateTime paymentDate)
        {
            return Create(paymentDate).Value;
        }

        public static implicit operator DateTime(PaymentDate paymentDate)
        {
            return paymentDate.Value;
        }
    }
}
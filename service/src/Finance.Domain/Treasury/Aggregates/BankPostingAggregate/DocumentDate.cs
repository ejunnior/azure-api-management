namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using System;
    using CSharpFunctionalExtensions;

    public class DocumentDate : Core.ValueObject<DocumentDate>
    {
        private DocumentDate()
        {
        }

        private DocumentDate(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }

        public static Result<DocumentDate> Create(Maybe<DateTime> invoiceDateOrNothing)
        {
            return invoiceDateOrNothing.ToResult("Invoice Date should not be empty")
                .Map(invoiceDate => new DocumentDate(invoiceDate));
        }

        public static explicit operator DocumentDate(DateTime invoiceDate)
        {
            return Create(invoiceDate).Value;
        }

        public static implicit operator DateTime(DocumentDate invoiceDate)
        {
            return invoiceDate.Value;
        }
    }
}
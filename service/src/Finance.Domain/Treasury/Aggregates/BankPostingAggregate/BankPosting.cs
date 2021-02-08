namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using System;
    using Bank.Aggregates.BankAccountAggregate;
    using Category.Aggregates.CategoryAggregate;
    using Core;
    using Creditor.Aggregates.CreditorAggregate;
    using CSharpFunctionalExtensions;

    public class BankPosting : AggregateRoot
    {
        private DateTime? _documentDate;
        private string _documentNumber;
        private DateTime? _paymentDate;

        public BankPosting(
            Amount amount,
            DueDate dueDate,
            Creditor creditor,
            Description description,
            Maybe<DocumentDate> documentDate,
            Maybe<DocumentNumber> documentNumber,
            BankAccount bankAccount,
            Category catgory,
            Maybe<PaymentDate> paymentDate,
            BankPostingType type)
        {
            Amount = amount;
            DueDate = dueDate;
            Description = description;
            DocumentDate = documentDate;
            DocumentNumber = documentNumber;
            BankAccount = bankAccount;
            Cateogry = catgory;
            PaymentDate = paymentDate;
            Creditor = creditor;
            Type = type;
        }

        private BankPosting()
        {
        }

        public Amount Amount { get; private set; }

        public BankAccount BankAccount { get; private set; }

        public Category Cateogry { get; private set; }

        public Creditor Creditor { get; private set; }
        public Description Description { get; private set; }

        public Maybe<DocumentDate> DocumentDate
        {
            get => _documentDate == null ? null : (DocumentDate)_documentDate;
            protected set => _documentDate = value.Unwrap(documentDate => documentDate.Value, default(DateTime?));
        }

        public Maybe<DocumentNumber> DocumentNumber
        {
            get => _documentNumber == null ? null : (DocumentNumber)_documentNumber;
            protected set => _documentNumber = value.Unwrap(documentNumber => documentNumber.Value);
        }

        public DueDate DueDate { get; protected set; }

        public Maybe<PaymentDate> PaymentDate
        {
            get => _paymentDate == null ? null : (PaymentDate)_paymentDate;
            protected set => _paymentDate = value.Unwrap(paymentDate => paymentDate.Value, default(DateTime?));
        }

        public BankPostingType Type { get; private set; }

        public void Edit(
            Amount amount,
            DueDate dueDate,
            Creditor creditor,
            Description description,
            Maybe<DocumentDate> documentDate,
            Maybe<DocumentNumber> documentNumber,
            BankAccount bankAccount,
            Category category,
            Maybe<PaymentDate> paymentDate,
            BankPostingType type)
        {
            Amount = amount;
            DueDate = dueDate;
            Creditor = creditor;
            Description = description;
            DocumentDate = documentDate;
            DocumentNumber = documentNumber;
            BankAccount = bankAccount;
            Cateogry = category;
            PaymentDate = paymentDate;
            Type = type;
        }
    }
}
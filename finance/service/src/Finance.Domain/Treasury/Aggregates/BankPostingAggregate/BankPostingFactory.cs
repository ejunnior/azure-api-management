namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using CSharpFunctionalExtensions;
    using System;
    using Bank.Aggregates.BankAccountAggregate;
    using Category.Aggregates.CategoryAggregate;
    using Creditor.Aggregates.CreditorAggregate;

    public static class BankPostingFactory
    {
        public static Result<BankPosting> Create(
            decimal amount,
            DateTime dueDate,
            DateTime? documentDate,
            string documentNumber,
            Creditor creditor,
            string description,
            BankAccount bankAccount,
            Category category,
            DateTime? paymentDate,
            BankPostingType type)
        {
            var totalAmountResult = Amount.Create(amount);
            var dueDateResult = DueDate.Create(dueDate);
            var documentDateResult = GetDocumentDate(documentDate);
            var documentNumberResult = GetDocumentNumber(documentNumber);
            var descriptionResult = Description.Create(description);
            var paymentDateResult = GetPaymentDate(paymentDate);

            var result = Result.Combine(
                totalAmountResult,
                dueDateResult,
                documentDateResult,
                documentNumberResult,
                descriptionResult,
                paymentDateResult);

            if (result.IsSuccess)
            {
                return Result.Success<BankPosting>(
                    new BankPosting(
                        amount: totalAmountResult.Value,
                        dueDate: dueDateResult.Value,
                        creditor: creditor,
                        description: descriptionResult.Value,
                        documentDate: documentDateResult.Value,
                        documentNumber: documentNumberResult.Value,
                        bankAccount: bankAccount,
                        catgory: category,
                        paymentDate: paymentDateResult.Value,
                        type: type));
            }

            return Result
                .Failure<BankPosting>(result.Error);
        }

        //TODO : need to be improved
        public static Result<Maybe<DocumentDate>> GetDocumentDate(DateTime? documentDateOrNothing)
        {
            if (documentDateOrNothing == null)
                return Result.Success<Maybe<DocumentDate>>(null);

            return DocumentDate
                .Create(documentDateOrNothing.Value)
                .Map(documentDate => (Maybe<DocumentDate>)documentDate);
        }

        //TODO : need to be improved
        public static Result<Maybe<DocumentNumber>> GetDocumentNumber(string documentNumberOrNothing)
        {
            if (documentNumberOrNothing == null)
                return Result.Success<Maybe<DocumentNumber>>(null);

            return DocumentNumber
                .Create(documentNumberOrNothing)
                .Map(documentNumber => (Maybe<DocumentNumber>)documentNumber);
        }

        //TODO : need to be improved
        public static Result<Maybe<PaymentDate>> GetPaymentDate(DateTime? paymentDateOrNothing)
        {
            if (paymentDateOrNothing == null)
                return Result.Success<Maybe<PaymentDate>>(null);

            return PaymentDate
                .Create(paymentDateOrNothing.Value)
                .Map(paymentDate => (Maybe<PaymentDate>)paymentDate);
        }
    }
}
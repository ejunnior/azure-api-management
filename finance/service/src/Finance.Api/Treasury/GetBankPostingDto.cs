namespace Finance.Api.Treasury
{
    using System;

    public class GetBankPostingDto
    {
        public decimal Amount { get; set; }

        public string BankAccount { get; set; }

        public string Category { get; set; }

        public string Creditor { get; set; }

        public string Description { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DueDate { get; set; }

        public Guid Id { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime PostingDate { get; set; }

        public string Status { get; set; }

        public string Type { get; set; }
    }
}
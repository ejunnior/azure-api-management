namespace Finance.Api.Treasury
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Treasury.Aggregates.BankPostingAggregate;
    using Validation;

    public class RegisterBankPostingDto
    {
        [Required, AmountValidation]
        public decimal Amount { get; set; }

        [Required]
        public Guid BankAccountId { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid CreditorId { get; set; }

        [Required, DescriptionValidation]
        public string Description { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        [Required]
        public BankPostingType Type { get; set; } //TODO: this is not good. Should be improved
    }
}
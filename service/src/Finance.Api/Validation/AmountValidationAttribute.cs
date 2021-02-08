namespace Finance.Api.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain.Treasury.Aggregates.BankPostingAggregate;

    [AttributeUsage(AttributeTargets.Property)]
    public class AmountValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var amount = decimal.Parse(value.ToString());

            var result = Amount
                .Create(amount);

            return result.IsFailure ? new ValidationResult(result.Error) : ValidationResult.Success;
        }
    }
}
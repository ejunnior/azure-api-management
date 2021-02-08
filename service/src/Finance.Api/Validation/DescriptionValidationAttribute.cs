namespace Finance.Api.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Domain;
    using Domain.Treasury.Aggregates.BankPostingAggregate;

    [AttributeUsage(AttributeTargets.Property)]
    public class DescriptionValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (!(value is string description))
                return new ValidationResult(Errors.General.ValueIsInvalid().Serialize());

            var result = Description
                .Create(description);

            if (result.IsFailure)
                return new ValidationResult(result.Error);

            return ValidationResult.Success;
        }
    }
}
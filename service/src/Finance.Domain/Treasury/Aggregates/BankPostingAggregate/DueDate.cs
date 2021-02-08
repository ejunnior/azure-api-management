namespace Finance.Domain.Treasury.Aggregates.BankPostingAggregate
{
    using System;
    using Core;
    using CSharpFunctionalExtensions;

    public class DueDate
    {
        private DueDate()
        {
        }

        private DueDate(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }

        public static Result<DueDate> Create(DateTime value)
        {
            return Result.Success<DueDate>(new DueDate(value));
        }

        public static explicit operator DueDate(DateTime dueDate)
        {
            return Create(dueDate).Value;
        }

        public static implicit operator DateTime(DueDate dueDate)
        {
            return dueDate.Value;
        }
    }
}
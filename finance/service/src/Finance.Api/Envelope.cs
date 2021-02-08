namespace Finance.Api
{
    using System;

    public class Envelope<T>
    {
        protected internal Envelope(
            T result,
            string errorMessage,
            string fieldName)
        {
            Result = result;
            ErrorMessage = errorMessage;
            FieldName = fieldName;
            TimeGenerated = DateTime.UtcNow;
        }

        public string ErrorMessage { get; }

        public string FieldName { get; }

        public T Result { get; }

        public DateTime TimeGenerated { get; }
    }

    public sealed class Envelope : Envelope<string>
    {
        private Envelope(
            string errorMessage,
            string fieldName)
            : base(null, errorMessage, fieldName)
        {
        }

        public static Envelope Error(
            string errorMessage,
            string fieldName)
        {
            return new Envelope(errorMessage, fieldName);
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, null, null);
        }

        public static Envelope Ok()
        {
            return new Envelope(null, null);
        }
    }
}
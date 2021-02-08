namespace Finance.Domain.Core
{
    using System;

    public class Error : ValueObject<Error>
    {
        private const string Separator = "||";

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }

        public string Message { get; }

        public static Error Deserialize(string serialized)
        {
            string[] data = serialized.Split(
                new[] { Separator },
                StringSplitOptions.RemoveEmptyEntries);

            return new Error(data[0], data[1]);
        }

        public static implicit operator string(Error error)
        {
            return error.Message;
        }

        public string Serialize()
        {
            return $"{Code}{Separator}{Message}";
        }
    }
}
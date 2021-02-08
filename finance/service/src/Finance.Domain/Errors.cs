namespace Finance.Domain
{
    using Core;

    public static class Errors
    {
        public static class General
        {
            public static Error NotFound()
                => new Error(code: "record.not.found", message: $"Not found for Id");

            public static Error ValueIsInvalid()
                => new Error(code: "value.is.invalid", message: $"Value is invalid");

            public static Error ValueIsRequired()
                => new Error(code: "value.is.required", message: $"Value is required");

            public static Error ValueIsTooLong()
                => new Error(code: "value.is.toolong", message: $"Value is too long");
        }
    }
}
namespace Finance.Api
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ModelStateValidator
    {
        public static IActionResult ValidateModelState(ActionContext context)
        {
            (string fieldName, ModelStateEntry entry) = context.ModelState
                .First(x => x.Value.Errors.Count > 0);
            var errorSerialized = entry.Errors.First().ErrorMessage;

            var error = errorSerialized;
            var envelope = Envelope.Error(error, fieldName);

            var result = new BadRequestObjectResult(envelope);

            return result;
        }
    }
}
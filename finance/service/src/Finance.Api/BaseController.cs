namespace Finance.Api
{
#pragma warning disable CS1591

    using CSharpFunctionalExtensions;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid UserId
        {
            get
            {
                Guid userGuid;

                Guid.TryParse(
                    input: User.Claims.FirstOrDefault(claim => claim.Type == "sub").Value,
                    out userGuid);

                return userGuid;
            }
        }

        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage, ""));
        }

        protected IActionResult FromResult(Result result)
        {
            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }
    }

#pragma warning restore CS1591
}
namespace Finance.Api
{
    using Infrastructure.Data.UnitOfWork;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        public UnitOfWorkActionFilter()
        {
        }

        private IFinanceUnitOfWork UnitOfWork { get; set; }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            UnitOfWork = actionContext.HttpContext.RequestServices.GetService(typeof(IFinanceUnitOfWork)) as IFinanceUnitOfWork;
            UnitOfWork.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            UnitOfWork = actionExecutedContext.HttpContext.RequestServices.GetService(typeof(IFinanceUnitOfWork)) as IFinanceUnitOfWork;

            if (actionExecutedContext.Exception == null)
            {
                UnitOfWork.CommitAsync();
            }
            else
            {
                UnitOfWork.RollbackChangesAsync();
            }
        }
    }
}
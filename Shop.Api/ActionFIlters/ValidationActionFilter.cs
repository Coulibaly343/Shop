using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shop.Api.ActionFIlters {
    public class ValidationActionFilter : IActionFilter {
        public void OnActionExecuting (ActionExecutingContext filterContext) {
            if (!filterContext.ModelState.IsValid) {
                filterContext.Result = new BadRequestObjectResult (filterContext.ModelState);
            }
        }
        public void OnActionExecuted (ActionExecutedContext filterContext) { }
    }
}
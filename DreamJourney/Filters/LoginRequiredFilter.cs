using DreamJourney.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DreamJourney.Filters
{
    public class LoginRequiredFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices
                .GetService<IUserContextService>();

            if (userContext == null || !userContext.IsLoggedIn)
            {
                context.Result = new RedirectToActionResult(
                    "Login", "Users", null);
            }
        }
    }

}

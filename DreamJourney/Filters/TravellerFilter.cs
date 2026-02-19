using DreamJourney.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DreamJourney.Filters
{
    public class TravellerFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices
                .GetService<IUserContextService>();

            if (userContext == null || userContext.IsTripCreator)
            {
                context.Result = new RedirectToActionResult(
                    "List", "Trips", null);
            }
        }
    }
}

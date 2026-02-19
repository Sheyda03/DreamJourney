using DreamJourney.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamJourney.Filters
{
    public class TripCreatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userContext = context.HttpContext.RequestServices
                .GetService<IUserContextService>();

            if (userContext == null || !userContext.IsTripCreator)
            {
                context.Result = new RedirectToActionResult(
                    "List", "Trips", null);
            }
        }
    }

}

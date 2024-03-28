using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RealEstate.Models.Enums;

namespace RealEstate.Attributes
{
    public class AuthorizedBrokerAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var role = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != Role.Broker.ToString())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}

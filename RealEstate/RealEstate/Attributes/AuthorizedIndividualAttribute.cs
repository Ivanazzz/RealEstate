using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models.Enums;
using System.Security.Claims;

namespace RealEstate.Attributes
{
    public class AuthorizedIndividualAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var role = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != Role.Individual.ToString())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}

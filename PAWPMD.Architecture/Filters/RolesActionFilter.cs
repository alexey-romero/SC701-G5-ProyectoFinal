using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PAWPMD.Architecture.Filters
{
    public class RolesActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Cookies["AuthToken"];
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var roles = jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                context.HttpContext.Items["UserRoles"] = roles;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
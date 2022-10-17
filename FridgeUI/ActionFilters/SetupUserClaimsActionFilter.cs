using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FridgeUI.ActionFilters
{
    public class SetupUserClaimsActionFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = "";
            context.HttpContext.Request.Cookies.TryGetValue("token", out token);
            if (!token.IsNullOrEmpty())
            {
                var handler = new JwtSecurityTokenHandler();
                var Token = handler.ReadJwtToken(token);
                context.HttpContext.User.AddIdentity(new ClaimsIdentity(Token.Claims));
            }
        }
    }
}

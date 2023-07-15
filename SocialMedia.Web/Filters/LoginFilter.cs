using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace SocialMedia.Web.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userID = context.HttpContext.Session.GetString("ID");
            if (userID=="" || userID==null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"action","Login" },
                    { "controller","Auth"}
                });
            }
            base.OnActionExecuting(context);
        }
    }
}

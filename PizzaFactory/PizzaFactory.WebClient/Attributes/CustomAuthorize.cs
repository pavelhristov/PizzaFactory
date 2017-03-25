using System.Web.Mvc;
using System.Web.Routing;

namespace PizzaFactory.WebClient.Attributes
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { area = "", controller = "NotFound" }));
        }
    }
}
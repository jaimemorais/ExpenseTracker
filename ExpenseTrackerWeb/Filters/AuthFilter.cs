using System.Web.Mvc;
using System.Web.Routing;

namespace ExpenseTrackerWeb.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.Session["UserLoggedIn"] == null || !(bool)filterContext.HttpContext.Session["UserLoggedIn"])
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary{{ "controller", "Login" },
                                             { "action", "Index" }
                                        });
            }



            base.OnActionExecuting(filterContext);
        }
    }
}
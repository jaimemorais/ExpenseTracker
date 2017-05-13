using ExpenseTrackerWeb.Filters;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{

    [AuthFilter]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "ExpenseTracker";
            
            return View();
        }
    }
}

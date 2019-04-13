using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTrackerWebApi.Controllers.Mvc
{
    public class NewExpenseMvcController : Controller
    {
        // GET: NewExpense
        public ActionResult Index()
        {
            return View();
        }
    }
}
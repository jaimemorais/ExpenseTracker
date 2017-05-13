using ExpenseTrackerDomain.Models;
using ExpenseTrackerWeb.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{
    [AuthFilter]
    public class BalanceController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            try
            {

                List<Expense> expenses = await base.GetItemListAsync<Expense>("Expenses");
                
                return View(expenses);
            }
            catch (Exception e)
            {
                Trace.TraceError("BalanceController Index Error : " + e.Message);
                ShowMessage("Error getting expense list.", EnumMessageType.ERROR);
                return View();
            }

        }
    }
}
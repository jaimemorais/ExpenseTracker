using ExpenseTrackerDomain.Models;
using ExpenseTrackerWeb.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
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


        public async Task<ActionResult> BalanceTxt()
        {
            try
            {

                List<Expense> expenses = await base.GetItemListAsync<Expense>("Expenses");


                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("<table>");
                

                foreach (Expense exp in expenses)
                {
                    sb.AppendLine("<tr>");

                    string month = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(exp.Date.Month);
                    month = month.Substring(0, 1).ToUpper() + month.Substring(1);

                    string name = (exp.UserName.Split()[0] == "patricia" ? "Patrícia" : "Jaime");

                    sb.AppendLine(GetTD(month) +
                                  GetTD(exp.Date.Day.ToString()) +
                                  GetTD(exp.Value.ToString()) +
                                  GetTD(name) +
                                  GetTD(exp.Category) +
                                  GetTD(exp.Description) +
                                  GetTD((exp.PaymentType == "Cartao Credito" ? "Itau CC" + GetTD("Cartao de Credito") + GetTD(name) : exp.PaymentType))
                                 );

                    

                    sb.AppendLine("</tr>");
                }
                
                return Content(sb.ToString(), "text/html");
            }
            catch (Exception e)
            {
                Trace.TraceError("BalanceController Index Error : " + e.Message);
                ShowMessage("Error getting expense list.", EnumMessageType.ERROR);
                return View();
            }

        }

        private string GetTD(string str)
        {
            return "<td>" + str + "</td>";
        }

    }
}
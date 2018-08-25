using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace ExpenseTrackerWebApi.Controllers
{
    public class ExpensesHtmlReportController : BaseApiController
    {


        [HttpGet]
        public async Task<HttpResponseMessage> ExpensesReport(string apiToken, string username, string category = null)
        {            
            if (apiToken != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }


            try
            {
                MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

                List<Expense> expenseList =
                    await expenseHelper.Collection.Find(e => e.UserName == username)
                    .ToListAsync();

                if (!string.IsNullOrEmpty(category))
                {
                    expenseList = expenseList.Where(e => e.Category == category).ToList();
                }



                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");                
                
                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(CreateHtmlTable(expenseList), System.Text.Encoding.UTF8, "text/html")
                };

                return resp;
            }
            catch (Exception e)
            {
                Trace.TraceError($"{nameof(ExpensesHtmlReportController)} Error : " + e.Message);                
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        private string CreateHtmlTable(List<Expense> expenseList)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<table>");


            foreach (Expense exp in expenseList)
            {
                sb.AppendLine("<tr>");

                string month = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(exp.Date.Month);
                month = month.Substring(0, 1).ToUpper() + month.Substring(1);

                string name = (exp.UserName.Split('@')[0] == "patricia" ? "Patrícia" : "Jaime");

                sb.AppendLine(GetTD(month) +
                              GetTD(exp.Date.Day.ToString()) +
                              GetTD(exp.Value.ToString()) +
                              GetTD(name) +
                              GetTD(exp.Category) +
                              GetTD(exp.Description) +
                              GetTD(SetPaymentType(exp, name))
                             );



                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");

            return sb.ToString();
        }

        private string SetPaymentType(Expense exp, string name)
        {
            string customPaymentType = exp.PaymentType;

            if (exp.PaymentType == "Cartao Credito")
                 customPaymentType = "Itau CC" + GetTD("Cartao de Credito") + GetTD(name);
            else if (exp.PaymentType == "Ticket Supermercado")
                customPaymentType = "Ticket Alim" + GetTD("Sodexo");
            else if (exp.PaymentType == "Ticket Alim")
                customPaymentType = "Ticket Alim" + GetTD("Sodexo");
            else if (exp.PaymentType == "Ticket Rest")
                customPaymentType = "Ticket Rest" + GetTD("Sodexo");

            return customPaymentType;
        }

        private string GetTD(string str)
        {
            return "<td>" + str + "</td>";
        }
    }
}

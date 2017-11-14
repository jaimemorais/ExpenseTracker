using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;


namespace ExpenseTrackerWebApi.Controllers
{
    public class ExpensesTxtReportController : BaseApiController
    {


        [HttpGet]
        public async Task<HttpResponseMessage> ExpensesTxt(string apiToken)
        {            
            if (apiToken != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }


            try
            {
                MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

                List<Expense> expenseList =
                    await expenseHelper.Collection.Find(e => e.UserName == UtilApi.GetHeaderValue(Request, "CurrentUserName"))
                    .ToListAsync();                

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

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
                                  GetTD((exp.PaymentType == "Cartao Credito" ? "Itau CC" + GetTD("Cartao de Credito") + GetTD(name) : exp.PaymentType))
                                 );



                    sb.AppendLine("</tr>");
                }
                

                var resp = new HttpResponseMessage(HttpStatusCode.OK);
                resp.Content = new StringContent(sb.ToString(), System.Text.Encoding.UTF8, "text/plain");
                return resp;
            }
            catch (Exception e)
            {
                Trace.TraceError("ExpensesTxtReportController Error : " + e.Message);                
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        private string GetTD(string str)
        {
            return "<td>" + str + "</td>";
        }
    }
}

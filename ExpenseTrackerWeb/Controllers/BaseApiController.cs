using ExpenseTrackerWebApi.Helpers;
using System.Configuration;
using System.Net;
using System.Web.Http;

namespace ExpenseTrackerWebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected void CheckAuth()
        {
            // TODO auth - meanwhile I use this
            if (UtilApi.GetHeaderValue(Request, "expensetracker-api-token") == null ||
                UtilApi.GetHeaderValue(Request, "expensetracker-api-token") != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
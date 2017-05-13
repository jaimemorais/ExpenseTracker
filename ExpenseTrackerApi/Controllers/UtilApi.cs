using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ExpenseTrackerApi.Controllers
{
    public static class UtilApi
    {
        public static string GetHeaderValue(HttpRequestMessage request, string headerName)
        {
            string currentUserName = null;
            if (request.Headers.Contains(headerName))
            {
                IEnumerable<string> headerValues = request.Headers.GetValues(headerName);
                currentUserName = headerValues.FirstOrDefault();
            }


            return currentUserName;
        }
    }
}
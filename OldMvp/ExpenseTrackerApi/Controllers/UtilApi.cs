using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ExpenseTrackerApi.Controllers
{
    public static class UtilApi
    {
        public static string GetHeaderValue(HttpRequestMessage request, string headerName)
        {            
            if (request.Headers.Contains(headerName))
            {
                IEnumerable<string> headerValues = request.Headers.GetValues(headerName);
                return headerValues.FirstOrDefault();
            }

            return null;
        }
    }
}
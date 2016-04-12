using System;

using Android.App;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace AppAndroid
{

    public static class Configuration
    {
        #if DEBUG
            public const string WebApiServiceURL = "http://10.0.2.2:50805/api/";  // 10.0.2.2 = localhost - emulator
        #else
            public const string WebApiServiceURL = "release string";
        #endif
    }


    [Activity(Label = "BaseExpenseTrackerActivity")]
    public class BaseExpenseTrackerActivity : Activity
    {

        protected string GetApiServiceURL(string apiId)
        {
            try
            {
                return Configuration.WebApiServiceURL + apiId;                    
            }
            catch
            {
                throw new Exception("WebApiServiceURL not set.");
            }
        }


        protected static JArray GetJsonData(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            var response = httpClient.GetAsync(url);

            if (response.Result.IsSuccessStatusCode)
            {
                var responseContent = response.Result.Content;

                string responseString = responseContent.ReadAsStringAsync().Result;

                JArray jArray = JArray.Parse(responseString);

                return jArray;
            }

            return null;
        }

    }
}
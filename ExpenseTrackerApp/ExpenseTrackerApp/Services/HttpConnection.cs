using ExpenseTrackerApp.Converters;
using ExpenseTrackerApp.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public class HttpConnection : IHttpConnection
    {

        private readonly JsonSerializer _jsonSerializer;
        private readonly IUserSettings _userSettings;
        private readonly ITelemetry _telemetry;

        public HttpConnection(IUserSettings userSettings, ITelemetry telemetry)
        {
            _jsonSerializer = new JsonSerializer();
            _jsonSerializer.Converters.Add(new StringEnumConverter());
            _jsonSerializer.Converters.Add(new ExpenseTrackerJsonDateConverter());
            _jsonSerializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _jsonSerializer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            _jsonSerializer.NullValueHandling = NullValueHandling.Ignore;

            _userSettings = userSettings;

            _telemetry = telemetry;
        }


        private HttpClient CreateHttpClient(Uri uri, ModernHttpClient.NativeMessageHandler handler)
        {
            HttpClient httpClient = new HttpClient(handler);

            httpClient.Timeout = new TimeSpan(0, 0, 0, 0, AppSettings.CONNECTION_TIMEOUT);

            httpClient.DefaultRequestHeaders.Add("CurrentUserName", _userSettings.GetEmail());

            httpClient.DefaultRequestHeaders.Add("expensetracker-api-token", AppSettings.API_TOKEN);


            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }


        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {                
                Uri serviceUri = new Uri(uri);
                using (var handler = new ModernHttpClient.NativeMessageHandler())
                using (HttpClient httpClient = CreateHttpClient(serviceUri, handler))
                {
                    HttpResponseMessage response = await httpClient.GetAsync(uri);

                    return await ProcessResponse<T>(null, response);
                }
            }
            catch (Exception ex)
            {                
                _telemetry.LogError(string.Empty, ex);

                return default(T);
            }
        }


        public async Task<T> PostAsync<T>(string uri, object objPost)
        {
            try
            {
                Uri serviceUri = new Uri(uri);
                using (var handler = new ModernHttpClient.NativeMessageHandler())
                using (HttpClient httpClient = CreateHttpClient(serviceUri, handler))
                {   
                    #if DEBUG
                    Debug.WriteLine("** JSON POST " + uri);
                    Debug.WriteLine(JObject.Parse(JsonConvert.SerializeObject(objPost)).ToString());
                    #endif

                    StringContent content = new StringContent(JsonConvert.SerializeObject(objPost));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                    return await ProcessResponse<T>(content, response);
                }
            }
            catch (Exception ex)
            {
                _telemetry.LogError(string.Empty, ex);

                return default(T);
            }
        }


        private async Task<T> ProcessResponse<T>(StringContent content, HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                #if DEBUG
                Debug.WriteLine("** JSON RESPONSE: ");
                var jsonRetorno = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine(JObject.Parse(jsonRetorno).ToString());
                #endif
                
                T retorno;
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    retorno = _jsonSerializer.Deserialize<T>(json);
                }

                return retorno;
            }
            else
            {
                // TODO process HTTP <> 200

                return default(T);
            }
        }

    }
}

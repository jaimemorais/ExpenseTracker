﻿using ExpenseTrackerApp.Converters;
using ExpenseTrackerApp.Helpers;
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


        private HttpClient CreateHttpClient()
        {
            // Using android handler, config in ExpenseTrackerApp.Droid.csproj (Properties / Android Options / Advanced)
            // <AndroidHttpClientHandlerType>Xamarin.Android.Net.AndroidClientHandler</AndroidHttpClientHandlerType>
            // <AndroidTlsProvider>btls</AndroidTlsProvider>             

            HttpClient httpClient = new HttpClient()
            {
                Timeout = new TimeSpan(0, 0, 0, 0, 10000)
            };

            httpClient.DefaultRequestHeaders.Add("CurrentUserName", _userSettings.GetEmail());

            httpClient.DefaultRequestHeaders.Add("expensetracker-api-token", Secrets.API_TOKEN);


            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }


        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(uri);

                    return await ProcessResponse<T>(null, response);
                }
            }
            catch (Exception ex)
            {                
                _telemetry.LogError("HttpConnection.GetAsync Error", ex);

                return default(T);
            }
        }


        public async Task<bool> PostJsonAsync<T>(string uri, object objPost)
        {
            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {   
                    #if DEBUG
                    Debug.WriteLine("** JSON POST " + uri);
                    Debug.WriteLine(JObject.Parse(JsonConvert.SerializeObject(objPost)).ToString());
                    #endif

                    StringContent content = new StringContent(JsonConvert.SerializeObject(objPost));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                _telemetry.LogError("HttpConnection.PostJsonAsync Error", ex);

                return false;
            }
        }


        public async Task<bool> PostStringContentAsync<T>(string uri, string content)
        {
            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(content));

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                _telemetry.LogError("HttpConnection.PostStringContentAsync Error", ex);

                return false;
            }
        }



        public async Task<bool> DeleteAsync(string uri)
        {
            try
            {
                using (HttpClient httpClient = CreateHttpClient())
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync(uri);

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                _telemetry.LogError("HttpConnection.DeleteAsync Error", ex);

                return false;
            }
        }


        private async Task<T> ProcessResponse<T>(StringContent content, HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.OK)
            {
                #if DEBUG
                Debug.WriteLine("** JSON RESPONSE: ");
                var outputJson = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(JArray.Parse(outputJson).ToString());
                #endif
                
                T obj;
                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    obj = _jsonSerializer.Deserialize<T>(json);
                }

                return obj;
            }
            else
            {
                // TODO 

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Invalid api token");
                }

                return default(T);
            }
        }

    }
}

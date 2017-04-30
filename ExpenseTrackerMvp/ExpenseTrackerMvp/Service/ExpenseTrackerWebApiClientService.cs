using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.Service
{
    public class ExpenseTrackerWebApiClientService : IExpenseTrackerWebApiClientService
    {


        protected string GetApiServiceURL(string apiId)
        {
            try
            {
                // AVD
                // 10.0.2.2 = localhost - emulator
                // IIS, not iisexpress 

                // Hyper v 
                // http://briannoyesblog.azurewebsites.net/2016/03/06/calling-localhost-web-apis-from-visual-studio-android-emulator/
                // enable firewall rule for port 80
                // http://169.254.80.80  (Desktop Adapter #2 )

                return AppConfig.Instance.GetExpenseTrackerApiUrl() + apiId;
            }
            catch
            {
                throw new Exception("expensetracker-api-url not set.");
            }
        }


        protected HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            httpClient.Timeout = new System.TimeSpan(0, 0, 3);
            return httpClient;
        }


        public async Task<List<Model.Category>> GetCategoryList()
        {
            List<Model.Category> CategoryList = new List<Model.Category>();

            using (HttpClient httpClient = GetHttpClient())
            {
                var response = await httpClient.GetAsync(GetApiServiceURL("Categories"));

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var responseContent = await content.ReadAsStringAsync();

                    JArray json = JArray.Parse(responseContent);

                    foreach (JToken item in json)
                    {
                        JObject cat = (JObject)JsonConvert.DeserializeObject(item.ToString());

                        Category catItem = new Model.Category();
                        catItem.Name = cat["Name"].ToString();

                        CategoryList.Add(catItem);
                    }
                }
            }

            return CategoryList;
        }

        public async Task<List<Expense>> GetExpenseList()
        {
            List<Expense> returnList = new List<Expense>();

            using (HttpClient httpClient = GetHttpClient())
            {
                var response = await httpClient.GetAsync(GetApiServiceURL("Expenses"));

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content;
                    var responseContent = await content.ReadAsStringAsync();

                    JArray json = JArray.Parse(responseContent);

                    foreach (JToken item in json)
                    {
                        Expense exp = new Expense();
                        JObject e = (JObject)JsonConvert.DeserializeObject(item.ToString());
                        exp.Description = e["Description"].ToString();
                        exp.Value = double.Parse(e["Value"].ToString());
                        exp.Date = DateTime.Parse(e["Date"].ToString());
                        exp.PaymentType = e["PaymentType"]?.ToString();
                        exp.Category = e["Category"]?.ToString();

                        returnList.Add(exp);
                    }
                }
            }
           

            return returnList.OrderByDescending(e => e.Date).ToList();
        }


        public async Task<HttpResponseMessage> SaveExpense(Expense expense)
        {
            string json = JsonConvert.SerializeObject(expense);

            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = await GetHttpClient().PostAsync(GetApiServiceURL("Expenses"), httpContent);

            return httpResponse;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using ExpenseTrackerWeb.Models;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace ExpenseTrackerWeb.Controllers
{
    public class BalanceController : BaseController
    {
        public async Task<ActionResult> Index()
        {

            string url = string.Format("http://localhost:59277/api/ExpenseApi");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            JArray json = JArray.Parse(content);

            var expenseItems = new List<Expense>();

            foreach (JToken item in json)
            {
                MongoDB.Bson.BsonDocument document
                    = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(item.ToString());
                Expense e = BsonSerializer.Deserialize<Expense>(document);

                expenseItems.Add(e);
            }
                    


            return View(expenseItems);
        }
    }
}
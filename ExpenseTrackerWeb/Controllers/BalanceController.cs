using ExpenseTrackerDomain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{
    public class BalanceController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                string url = base.GetApiServiceURL("Expenses");
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
            catch (Exception e)
            {
                Trace.TraceError("BalanceController Index Error : " + e.Message);
                ShowMessage("Error getting expense list.", EnumMessageType.ERROR);
                return View();
            }

        }
    }
}
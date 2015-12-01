using ExpenseTrackerDomain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExpenseTrackerWeb.Controllers
{

    public enum EnumMessageType
    {
        INFO,
        ERROR
    }

    public abstract class BaseController : Controller
    {
        protected string GetApiServiceURL(string apiId) 
        {
            try
            {
                return ConfigurationManager.AppSettings["WebApiServiceURL"] + apiId;
            }
            catch
            {
                throw new Exception("WebApiServiceURL not set.");
            }
        }

        protected void ShowMessage(string msgText, EnumMessageType msgType)
        {
            TempData["MessageText"] = msgText;
            TempData["MessageType"] = msgType;
        }



        protected async Task<List<T>> GetItemListAsync<T>(string api) where T : MongoEntity
        {
            try
            {
                string url = this.GetApiServiceURL(api);
                Trace.TraceError("Api Service Url : " + url);

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                var response = await httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                JArray json = JArray.Parse(content);

                var items = new List<T>();

                foreach (JToken item in json)
                {
                    BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(item.ToString());
                    T itemDeserialized = BsonSerializer.Deserialize<T>(document);

                    items.Add(itemDeserialized);
                }

                return items;
            }
            catch (Exception e)
            {
                Trace.TraceError("BaseController.GetItemListAsync. (" + api + ") Error : " + e.Message);
                ShowMessage("Error getting list.", EnumMessageType.ERROR);
                return null;
            }
        }

    }
}
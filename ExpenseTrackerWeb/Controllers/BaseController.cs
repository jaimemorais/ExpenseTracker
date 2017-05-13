using ExpenseTrackerDomain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
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
                string content = await GetJsonResult(url);

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


        protected async Task<T> GetItemByIdAsync<T>(string api, string id) where T : MongoEntity
        {
            try
            {
                string url = this.GetApiServiceURL(api) + "/" + id;
                string content = await GetJsonResult(url);
                                
                JToken item = JToken.Parse(content);
                BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(item.ToString());
                T itemDeserialized = BsonSerializer.Deserialize<T>(document);

                return itemDeserialized;
            }
            catch (Exception e)
            {
                Trace.TraceError("BaseController.GetItemByIdAsync. (" + api + ") Error : " + e.Message);
                ShowMessage("Error getting item by Id.", EnumMessageType.ERROR);
                return null;
            }
        }


        private async Task<string> GetJsonResult(string url)
        {
            Trace.TraceInformation("Api Service Url : " + url);
            
            var response = await GetHttpClient().GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            httpClient.DefaultRequestHeaders.Add("expensetracker-api-token", ConfigurationManager.AppSettings.Get("expensetracker-api-token"));

            if (Session["UserName"] != null)
            {
                httpClient.DefaultRequestHeaders.Add("CurrentUserName", Session["UserName"].ToString());
            }

            return httpClient;
        }
    }
}
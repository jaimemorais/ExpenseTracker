using ExpenseTrackerWeb.Models;
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
            return ConfigurationManager.AppSettings["WebApiServiceURL"] + apiId;

        }

        protected void ShowMessage(string msgText, EnumMessageType msgType)
        {
            TempData["MessageText"] = msgText;
            TempData["MessageType"] = msgType;
        }



        protected async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                string url = this.GetApiServiceURL("CategoryApi");
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                var response = await httpClient.GetAsync(url);

                Trace.TraceError("Api Service Url : " + url);

                var content = await response.Content.ReadAsStringAsync();

                JArray json = JArray.Parse(content);

                var categories = new List<Category>();

                foreach (JToken item in json)
                {
                    BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(item.ToString());
                    Category c = BsonSerializer.Deserialize<Category>(document);

                    categories.Add(c);
                }

                return categories;
            }
            catch (Exception e)
            {
                Trace.TraceError("BaseController.GetCategoriesAsync Error : " + e.Message);
                ShowMessage("Error getting categories list.", EnumMessageType.ERROR);
                return null;
            }
        }


        protected async Task<List<PaymentType>> GetPaymentTypesAsync()
        {
            try
            {
                string url = this.GetApiServiceURL("PaymentTypeApi");
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
                var response = await httpClient.GetAsync(url);
                var content = await response.Content.ReadAsStringAsync();

                JArray json = JArray.Parse(content);

                var paymentTypes = new List<PaymentType>();

                foreach (JToken item in json)
                {
                    BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(item.ToString());
                    PaymentType c = BsonSerializer.Deserialize<PaymentType>(document);

                    paymentTypes.Add(c);
                }

                return paymentTypes;
            }
            catch (Exception e)
            {
                Trace.TraceError("BaseController.GetPaymentTypesAsync Error : " + e.Message);
                ShowMessage("Error getting Payment Types list.", EnumMessageType.ERROR);
                return null;
            }
        }

    }
}
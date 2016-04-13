using ExpenseTrackerDomain.Models;
using ExpenseTrackerWeb.Helpers;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpenseTrackerApi.Controllers.RestApi
{

    public class PaymentTypesController : ApiController
    {
        // GET api/PaymentTypes
        public async Task<IEnumerable<string>> GetAsync()
        {
            MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();
                
            IList<string> returnList = new List<string>();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            await paymentTypeHelper.Collection.Find(p => p.Name != null) // TODO filter by userId
                .ForEachAsync(paymentTypeDocument => 
                {
                    string docJson = paymentTypeDocument.ToJson(jsonWriterSettings);
                    returnList.Add(docJson);
                }
            );

            return returnList.ToArray();
        }

        // GET api/PaymentTypes/5
        public string Get(int id)
        {
            // TODO get one
            return "value";
        }

        // POST api/PaymentTypes
        public async Task PostAsync(PaymentType paymentTypePosted)
        {
            MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();

            try 
            {
                await paymentTypeHelper.Collection.InsertOneAsync(paymentTypePosted);
            }
            catch (Exception e)
            {
                Trace.TraceError("PaymentTypes PostAsync error : " + e.Message);
                throw;
            }

        }

        // PUT api/PaymentTypes/5
        public void Put(int id, [FromBody]string value)
        {            
            // TODO update
        }

        // DELETE api/PaymentTypes/5
        public void Delete(int id)
        {
            // TODO delete
        }
    }
}

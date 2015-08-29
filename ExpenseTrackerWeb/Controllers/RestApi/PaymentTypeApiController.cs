using ExpenseTrackerWeb.Helpers;
using ExpenseTrackerWeb.Models;
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

namespace ExpenseTrackerWeb.Controllers.RestApi
{

    public class PaymentTypeApiController : ApiController
    {
        // GET api/PaymentTypeApi
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

        // GET api/PaymentTypeApi/5
        public string Get(int id)
        {
            // TODO get one
            if (id != null)
            {
                // test
            }
            return "value";
        }

        // POST api/PaymentTypeApi
        public async Task PostAsync(PaymentType paymentTypePosted)
        {
            MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();

            try 
            {
                await paymentTypeHelper.Collection.InsertOneAsync(paymentTypePosted);
            }
            catch (Exception e)
            {
                Trace.TraceError("PaymentTypeApi PostAsync error : " + e.Message);
                throw;
            }

        }

        // PUT api/PaymentTypeApi/5
        public void Put(int id, [FromBody]string value)
        {            
            // TODO update
        }

        // DELETE api/PaymentTypeApi/5
        public void Delete(int id)
        {
            // TODO delete
        }
    }
}

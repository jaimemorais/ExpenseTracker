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

        // GET api/Categories
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

        // GET api/Categories/5
        public async Task<string> GetAsync(string id)
        {
            MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            PaymentType paymentType = await paymentTypeHelper.Collection
                .Find(p => p.Id.Equals(ObjectId.Parse(id))) // TODO filter by userId
                .FirstAsync();

            return paymentType.ToJson(jsonWriterSettings);

        }

        // POST api/Categories
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

        // PUT api/Categories/5
        public async Task PutAsync(string id, PaymentType paymentTypePut)
        {
            try
            {
                var filter = Builders<PaymentType>.Filter.Eq(p => p.Id, ObjectId.Parse(id));
                var update = Builders<PaymentType>.Update.Set("Name", paymentTypePut.Name);

                MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();
                await paymentTypeHelper.Collection.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {
                Trace.TraceError("PaymentTypes PutAsync error : " + e.Message);
                throw;
            }
        }

        // DELETE api/Categories/5
        public async Task DeleteAsync(string id)
        {
            try
            {
                var filter = Builders<PaymentType>.Filter.Eq(p => p.Id, ObjectId.Parse(id));

                MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();
                await paymentTypeHelper.Collection.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                Trace.TraceError("PaymentTypes DeleteAsync error : " + e.Message);
                throw;
            }
        }
    }
}

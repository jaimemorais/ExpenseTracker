using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTrackerWebApi.Controllers
{
    public class PaymentTypesController : BaseApiController
    {
        // GET api/PaymentTypes
        public async Task<IEnumerable<string>> GetAsync()
        {
            CheckAuth();

            MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();

            IList<string> returnList = new List<string>();
            await paymentTypeHelper.Collection.Find(p => p.UserName == UtilApi.GetHeaderValue(Request, "CurrentUserName"))
                .ForEachAsync(paymentTypeDocument =>
                {
                    string docJson = Newtonsoft.Json.JsonConvert.SerializeObject(paymentTypeDocument);
                    returnList.Add(docJson);
                }
            );

            return returnList.ToArray();
        }

        // GET api/PaymentTypes/5
        public async Task<string> GetAsync(string id)
        {
            CheckAuth();

            MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();

            PaymentType paymentType = await paymentTypeHelper.Collection
                .Find(p => p.Id.Equals(id))
                .FirstAsync();

            return Newtonsoft.Json.JsonConvert.SerializeObject(paymentType);

        }

        // POST api/PaymentTypes
        public async Task PostAsync(PaymentType paymentTypePosted)
        {
            CheckAuth();

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
        public async Task PutAsync(string id, PaymentType paymentTypePut)
        {
            CheckAuth();

            try
            {
                var filter = Builders<PaymentType>.Filter.Eq(p => p.Id, id);
                var update = Builders<PaymentType>.Update.Set("Name", paymentTypePut.Name)
                                                       .Set("UserName", paymentTypePut.UserName);

                MongoHelper<PaymentType> paymentTypeHelper = new MongoHelper<PaymentType>();
                await paymentTypeHelper.Collection.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {
                Trace.TraceError("PaymentTypes PutAsync error : " + e.Message);
                throw;
            }
        }

        // DELETE api/PaymentTypes/5
        public async Task DeleteAsync(string id)
        {
            CheckAuth();

            try
            {
                var filter = Builders<PaymentType>.Filter.Eq(p => p.Id, id);

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

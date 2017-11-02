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

    public class ExpenseApiController : ApiController
    {
        // GET api/ExpenseApi
        public async Task<IEnumerable<string>> GetAsync()
        {            
            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();
                
            IList<string> returnList = new List<string>();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            await expenseHelper.Collection.Find(e => e.Value > 0) // TODO filter by userId
                .ForEachAsync(expenseDocument => 
                {
                    string docJson = expenseDocument.ToJson(jsonWriterSettings);
                    returnList.Add(docJson);
                }
            );                    

            return returnList.ToArray();
        }

        // GET api/ExpenseApi/5
        public string Get(int id)
        {
            // TODO get one
            return "value";
        }

        // POST api/ExpenseApi
        public async Task PostAsync(Expense expensePosted)
        {
            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

            try
            {
                await expenseHelper.Collection.InsertOneAsync(expensePosted);
            }
            catch (Exception e)
            {
                Trace.TraceError("ExpenseApi PostAsync error : " + e.Message);
                throw;
            }
            
        }

        // PUT api/ExpenseApi/5
        public void Put(int id, [FromBody]string value)
        {
            // TODO update
        }

        // DELETE api/ExpenseApi/5
        public void Delete(int id)
        {
            // TODO delete
        }
    }
}

using ExpenseTrackerWeb.Helpers;
using ExpenseTrackerDomain.Models;
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

    public class CategoriesController : ApiController
    {
        // GET api/Categories
        public async Task<IEnumerable<string>> GetAsync()
        {
            MongoHelper<Category> categoryHelper = new MongoHelper<Category>();
                
            IList<string> returnList = new List<string>();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            await categoryHelper.Collection.Find(c => c.Name != null) // TODO filter by userId
                .ForEachAsync(categoryDocument => 
                {
                    string docJson = categoryDocument.ToJson(jsonWriterSettings);
                    returnList.Add(docJson);
                }
            );

            return returnList.ToArray();
        }

        // GET api/Categories/5
        public async Task<string> GetAsync(string id)
        {
            MongoHelper<Category> categoryHelper = new MongoHelper<Category>();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            
            Category cat = await categoryHelper.Collection
                .Find(c => c.Id.Equals(ObjectId.Parse(id))) // TODO filter by userId
                .FirstAsync(); 

            return cat.ToJson(jsonWriterSettings);
            
        }

        // POST api/Categories
        public async Task PostAsync(Category categoryPosted)
        {
            MongoHelper<Category> categoryHelper = new MongoHelper<Category>();

            try
            {
                await categoryHelper.Collection.InsertOneAsync(categoryPosted);
            }
            catch (Exception e)
            {
                Trace.TraceError("Categories PostAsync error : " + e.Message);
                throw;
            }

        }

        // PUT api/Categories/5
        public async Task PutAsync(string id, Category categoryPut)
        {
            try
            {
                var filter = Builders<Category>.Filter.Eq(c => c.Id, ObjectId.Parse(id));
                var update = Builders<Category>.Update.Set("Name", categoryPut.Name);

                MongoHelper<Category> categoryHelper = new MongoHelper<Category>();
                await categoryHelper.Collection.UpdateOneAsync(filter, update);                
            }
            catch (Exception e)
            {
                Trace.TraceError("Categories PutAsync error : " + e.Message);
                throw;
            }
        }

        // DELETE api/Categories/5
        public async Task DeleteAsync(string id)
        {
            try
            {
                var filter = Builders<Category>.Filter.Eq(c => c.Id, ObjectId.Parse(id));                

                MongoHelper<Category> categoryHelper = new MongoHelper<Category>();
                await categoryHelper.Collection.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                Trace.TraceError("Categories DeleteAsync error : " + e.Message);
                throw;
            }
        }
    }
}

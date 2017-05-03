using ExpenseTrackerDomain.Models;
using ExpenseTrackerWeb.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            await categoryHelper.Collection.Find(c => c.Name != null) // TODO filter by userName
                .ForEachAsync(categoryDocument => 
                {
                    string docJson = Newtonsoft.Json.JsonConvert.SerializeObject(categoryDocument);
                    returnList.Add(docJson);
                }
            );

            return returnList.ToArray();
        }

        // GET api/Categories/5
        public async Task<string> GetAsync(string id)
        {
            MongoHelper<Category> categoryHelper = new MongoHelper<Category>();
            
            Category cat = await categoryHelper.Collection
                .Find(c => c.Id.Equals(ObjectId.Parse(id))) // TODO filter by userName
                .FirstAsync(); 

            return Newtonsoft.Json.JsonConvert.SerializeObject(cat);
            
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
                var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
                var update = Builders<Category>.Update.Set("Name", categoryPut.Name)
                                                       .Set("UserName", categoryPut.UserName);

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
                var filter = Builders<Category>.Filter.Eq(c => c.Id, id);                

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

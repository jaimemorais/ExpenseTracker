using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ExpenseTrackerWebApi.Controllers
{
    public class CategoriesController : BaseApiController
    {

        // GET api/Categories
        public async Task<IEnumerable<Category>> GetAsync()
        {
            CheckAuth();

            MongoHelper<Category> categoryHelper = new MongoHelper<Category>();

            List<Category> categoryList =
                await categoryHelper.Collection.Find(e => e.UserName == UtilApi.GetHeaderValue(Request, "CurrentUserName"))
                .ToListAsync();


            // test
            // List<Expense> expenseList = new List<Expense>() { new Expense() { Date = DateTime.Now, UserName = "jaime", Description = "web test" } };


            return categoryList;            
        }

        // GET api/Categories/5
        public async Task<Category> GetAsync(string id)
        {
            CheckAuth();

            MongoHelper<Category> categoryHelper = new MongoHelper<Category>();

            Category cat = await categoryHelper.Collection
                .Find(c => c.Id.Equals(id))
                .FirstAsync();

            return cat;
        }

        // POST api/Categories
        public async Task PostAsync(Category categoryPosted)
        {
            CheckAuth();

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
            CheckAuth();

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
            CheckAuth();

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

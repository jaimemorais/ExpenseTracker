using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpenseTrackerWebApi.Controllers
{

    public class ExpensesController : ApiController
    {
        private void CheckAuth()
        {
            // TODO auth - meanwhile I use this
            if (UtilApi.GetHeaderValue(Request, "expensetracker-api-token") == null ||
                UtilApi.GetHeaderValue(Request, "expensetracker-api-token") != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }

        // GET api/Expenses
        public async Task<IEnumerable<Expense>> GetAsync()
        {
            CheckAuth();

            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();


            List<Expense> expenseList =
                await expenseHelper.Collection.Find(e => e.UserName == UtilApi.GetHeaderValue(Request, "CurrentUserName"))
                .ToListAsync();

            return expenseList;
        }

        // GET api/Expenses/5
        public async Task<Expense> GetAsync(string id)
        {
            CheckAuth();

            MongoHelper<Expense> categoryHelper = new MongoHelper<Expense>();            

            Expense exp = await categoryHelper.Collection
                .Find(c => c.Id.Equals(id)) 
                .FirstAsync();

            return exp;
        }

        // POST api/Expenses
        public async Task PostAsync(Expense expensePosted)
        {
            CheckAuth();

            MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

            try
            {
                await expenseHelper.Collection.InsertOneAsync(expensePosted);                
            }
            catch (Exception e)
            {
                Trace.TraceError("Expenses PostAsync error : " + e.Message);
                throw;
            }
            
        }

        // PUT api/Expenses/5
        public async Task PutAsync(string id, Expense expensePut)
        {
            CheckAuth();

            try
            {
                MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();

                var filter = Builders<Expense>.Filter.Eq(e => e.Id, id);
                var update = Builders<Expense>.Update.Set("Date", expensePut.Date)
                                                     .Set("Value", expensePut.Value)
                                                     .Set("Description", expensePut.Description)
                                                     .Set("Category", expensePut.Category)
                                                     .Set("PaymentType", expensePut.PaymentType)
                                                     .Set("UserName", expensePut.UserName);
                                                     
                await expenseHelper.Collection.UpdateOneAsync(filter, update);                                
            }
            catch (Exception e)
            {
                Trace.TraceError("Expenses PutAsync error : " + e.Message);
                throw;
            }
        }

        // DELETE api/Expenses/5
        public async Task DeleteAsync(string id)
        {
            CheckAuth();

            try
            {
                var filter = Builders<Expense>.Filter.Eq(c => c.Id, id);

                MongoHelper<Expense> expenseHelper = new MongoHelper<Expense>();
                await expenseHelper.Collection.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                Trace.TraceError("Expenses DeleteAsync error : " + e.Message);
                throw;
            }
        }
    }
}

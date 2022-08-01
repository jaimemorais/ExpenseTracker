using ExpenseTrackerApp.Helpers;
using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Service
{
    public class ExpenseTrackerService : IExpenseTrackerService
    {
        private readonly IHttpConnection _httpConnection;
        private readonly ITelemetry _telemetry;

        public ExpenseTrackerService(IHttpConnection httpConnection, ITelemetry telemetry)
        {
            _httpConnection = httpConnection;
            _telemetry = telemetry;
        }
        

        public async Task<List<Category>> GetCategoryListAsync()
        {
            string uri = Secrets.BASE_API_ENDPOINT + "Categories";

            List<Category> categoryList = await _httpConnection.GetAsync< List<Category>>(uri);
           
            return categoryList.OrderBy(c => c.Name).ToList();
        }


        public async Task<List<PaymentType>> GetPaymentTypeListAsync()
        {
            string uri = Secrets.BASE_API_ENDPOINT + "PaymentTypes";

            List<PaymentType> paymentTypeList = await _httpConnection.GetAsync< List<PaymentType>>(uri);

            return paymentTypeList.OrderBy(p => p.Name).ToList();            
        }



        public async Task<List<Expense>> GetExpenseListAsync()
        {
            string uri = Secrets.BASE_API_ENDPOINT + "Expenses";

            List<Expense> expenseList = await _httpConnection.GetAsync<List<Expense>>(uri);
            
            return expenseList.OrderByDescending(e => e.Date).ToList();
        }


        public async Task<bool> SaveExpenseAsync(Expense expense)
        {
            string uri = Secrets.BASE_API_ENDPOINT + "Expenses";

            return await _httpConnection.PostJsonAsync<Expense>(uri, expense);            
        }


        public async Task<bool> DeleteExpenseAsync(Expense expense)
        {
            string uri = Secrets.BASE_API_ENDPOINT + "Expenses";

            return await _httpConnection.DeleteAsync(uri + "/" + expense.Id);            
        }



        public async Task<bool> UpdateUserFCMToken(string token)
        {
            string uri = Secrets.BASE_API_ENDPOINT + "Push";
            return await _httpConnection.PostStringContentAsync<string>(uri, token);                        
        }


    }
}

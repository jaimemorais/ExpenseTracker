using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Models.Service;
using ExpenseTrackerApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Service
{
    public class ExpenseTrackerService : IExpenseTrackerService
    {
        private readonly IHttpConnection _httpConnection;

        public ExpenseTrackerService(IHttpConnection httpConnection)
        {
            _httpConnection = httpConnection;
        }
        

        public async Task<List<Category>> GetCategoryListAsync()
        {
            CategoryResponse categoryResponse = await _httpConnection.GetAsync<CategoryResponse>(AppSettings.CategoryEndpoint);
            
            List<Category> categoryList = categoryResponse.CategoryList;
            
            return categoryList.OrderBy(c => c.Name).ToList();
        }


        public async Task<List<PaymentType>> GetPaymentTypeListAsync()
        {
            PaymentTypeResponse paymentTypeResponse = await _httpConnection.GetAsync<PaymentTypeResponse>(AppSettings.PaymentTypeEndpoint);

            List<PaymentType> paymentTypeList = paymentTypeResponse.PaymentTypeList;

            return paymentTypeList.OrderBy(p => p.Name).ToList();            
        }



        public async Task<List<Expense>> GetExpenseListAsync()
        {
            ExpenseResponse expenseResponse = await _httpConnection.GetAsync<ExpenseResponse>(AppSettings.ExpenseEndpoint);

            List<Expense> expenseList = expenseResponse.ExpenseList;

            return expenseList.OrderByDescending(e => e.Date).ToList();
        }


        public async Task<HttpResponseMessage> SaveExpenseAsync(Expense expense)
        {
            var response = await _httpConnection.PostAsync<BaseResponse>(AppSettings.ExpenseEndpoint, expense);

            // TODO
            //if (response.)

            return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
        }


        public async Task<HttpResponseMessage> DeleteExpenseAsync(Expense expense)
        {
            // TODO 
            // await _httpConnection.DeleteAsync()

            //HttpResponseMessage httpResponse = 
            //  await GetHttpClient().DeleteAsync(GetApiServiceURL("Expenses") + "/" + expense.Id);

            await Task.Delay(1);

            return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.InternalServerError };
        }
    }
}

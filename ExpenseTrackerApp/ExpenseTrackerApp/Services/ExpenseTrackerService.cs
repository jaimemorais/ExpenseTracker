using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Models.Service;
using ExpenseTrackerApp.Services;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<bool> SaveExpenseAsync(Expense expense)
        {
            var response = await _httpConnection.PostAsync<BaseResponse>(AppSettings.ExpenseEndpoint, expense);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _telemetry.LogError($"Error calling SaveExpenseAsync. StatusCode = {response.StatusCode}");
                return false;
            }            
        }


        public async Task<bool> DeleteExpenseAsync(Expense expense)
        {
            // TODO 
            // await _httpConnection.DeleteAsync()

            //HttpResponseMessage httpResponse = 
            //  await GetHttpClient().DeleteAsync(GetApiServiceURL("Expenses") + "/" + expense.Id);

            await Task.Delay(1);

            return false;
        }
    }
}

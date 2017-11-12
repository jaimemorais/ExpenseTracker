using ExpenseTrackerApp.Model;
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
            List<Category> categoryList = await _httpConnection.GetAsync< List<Category>>(AppSettings.CategoryEndpoint);
           
            return categoryList.OrderBy(c => c.Name).ToList();
        }


        public async Task<List<PaymentType>> GetPaymentTypeListAsync()
        {
            List<PaymentType> paymentTypeList = await _httpConnection.GetAsync< List<PaymentType>>(AppSettings.PaymentTypeEndpoint);

            return paymentTypeList.OrderBy(p => p.Name).ToList();            
        }



        public async Task<List<Expense>> GetExpenseListAsync()
        {
            List<Expense> expenseList = await _httpConnection.GetAsync<List<Expense>>(AppSettings.ExpenseEndpoint);
            
            return expenseList.OrderByDescending(e => e.Date).ToList();
        }


        public async Task<bool> SaveExpenseAsync(Expense expense)
        {
            return await _httpConnection.PostAsync<Expense>(AppSettings.ExpenseEndpoint, expense);            
        }


        public async Task<bool> DeleteExpenseAsync(Expense expense)
        {
            return await _httpConnection.DeleteAsync(AppSettings.ExpenseEndpoint + "/" + expense.Id);            
        }
    }
}

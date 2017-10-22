using ExpenseTrackerApp.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Service
{
    public interface IExpenseTrackerService
    {

        Task<List<Expense>> GetExpenseListAsync();

        Task<List<Category>> GetCategoryListAsync();

        Task<List<PaymentType>> GetPaymentTypeListAsync();

        Task<HttpResponseMessage> SaveExpenseAsync(Expense expense);

        Task<HttpResponseMessage> DeleteExpenseAsync(Expense expense);
    }
}

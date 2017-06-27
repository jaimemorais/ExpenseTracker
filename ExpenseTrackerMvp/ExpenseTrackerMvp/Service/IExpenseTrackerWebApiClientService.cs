using ExpenseTrackerMvp.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.Service
{
    public interface IExpenseTrackerWebApiClientService
    {

        Task<List<Expense>> GetExpenseListAsync();

        Task<List<Category>> GetCategoryListAsync();

        Task<List<PaymentType>> GetPaymentTypeListAsync();

        Task<HttpResponseMessage> SaveExpenseAsync(Expense expense);

        Task<HttpResponseMessage> DeleteExpenseAsync(Expense expense);
    }
}

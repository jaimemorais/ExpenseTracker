using ExpenseTrackerApp.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Service
{
    public interface IExpenseTrackerService
    {

        Task<List<Expense>> GetExpenseListAsync();

        Task<List<Category>> GetCategoryListAsync();

        Task<List<PaymentType>> GetPaymentTypeListAsync();

        Task<bool> SaveExpenseAsync(Expense expense);

        Task<bool> DeleteExpenseAsync(Expense expense);
    }
}

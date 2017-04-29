using ExpenseTrackerMvp.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.Service
{
    public interface IExpenseTrackerWebApiClientService
    {

        Task<List<Expense>> GetExpenseList();

        Task<List<Category>> GetCategoryList();

        Task<HttpResponseMessage> SaveExpense(Expense expense);
    }
}

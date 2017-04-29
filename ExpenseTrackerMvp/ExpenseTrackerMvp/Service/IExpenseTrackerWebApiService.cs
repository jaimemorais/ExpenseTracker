using ExpenseTrackerMvp.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.Service
{
    public interface IExpenseTrackerWebApiService
    {

        Task<ObservableCollection<Expense>> GetExpenseList();

        Task<List<Category>> GetCategoryList();

        Task<HttpResponseMessage> SaveExpense(Expense expense);
    }
}

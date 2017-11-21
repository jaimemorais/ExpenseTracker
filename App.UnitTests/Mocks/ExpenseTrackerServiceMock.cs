using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.UnitTests
{
    internal class ExpenseTrackerServiceMock : IExpenseTrackerService
    {
        
        public ExpenseTrackerServiceMock()
        {
        
        }

        public Task<bool> DeleteExpenseAsync(Expense expense)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Category>> GetCategoryListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Expense>> GetExpenseListAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<PaymentType>> GetPaymentTypeListAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveExpenseAsync(Expense expense)
        {
            // TODO
            await Task.Delay(0);            
            return true;
        }
    }
}
using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Service;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.ViewModels
{
    public class ExpenseListPageViewModel : BaseViewModel, INavigatedAware
    {

        public ObservableCollection<Expense> ExpenseList { get; set; }

        protected DelegateCommand LoadExpensesCommand => new DelegateCommand(async () => await ExecuteLoadExpensesAsync());


        private readonly IExpenseTrackerService _expenseTrackerService;

        public ExpenseListPageViewModel(IExpenseTrackerService expenseTrackerService)
        {
            _expenseTrackerService = expenseTrackerService;

            ExpenseList = new ObservableCollection<Expense>();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }


        public async void OnNavigatedTo(NavigationParameters parameters)
        {

            IsBusy = true;


            try
            {
                await ExecuteLoadExpensesAsync();                
            }
            catch
            {
                // TODO telemetry
            }
            finally
            {
                IsBusy = false;
            }


        }

        private async Task ExecuteLoadExpensesAsync()
        {
            ExpenseList.Clear();

            IList<Expense> list = await _expenseTrackerService.GetExpenseListAsync();

            foreach (Expense e in list)
            {
                ExpenseList.Add(e);
            }
        }
    }
}

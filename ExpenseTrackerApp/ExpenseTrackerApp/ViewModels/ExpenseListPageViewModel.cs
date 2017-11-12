using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Views;
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

        public DelegateCommand LoadExpensesCommand => new DelegateCommand(async () => await ExecuteLoadExpensesAsync());

        public DelegateCommand AddExpenseCommand => new DelegateCommand(async () => await AddExpenseAsync());


        private readonly IExpenseTrackerService _expenseTrackerService;
        private readonly INavigationService _navigationService;

        public ExpenseListPageViewModel(IExpenseTrackerService expenseTrackerService, INavigationService navigationService)
        {
            _expenseTrackerService = expenseTrackerService;
            _navigationService = navigationService;

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


        private async Task AddExpenseAsync()
        {
            await _navigationService.NavigateAsync(nameof(ExpenseCreatePage), null, true);
        }
    }
}

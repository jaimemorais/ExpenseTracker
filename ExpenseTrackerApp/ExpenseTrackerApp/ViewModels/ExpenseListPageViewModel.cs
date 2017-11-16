using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Views;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackerApp.ViewModels
{
    public class ExpenseListPageViewModel : BaseViewModel, INavigatedAware
    {

        public ObservableCollection<Expense> ExpenseList { get; set; }

        public DelegateCommand LoadExpensesCommand => new DelegateCommand(async () => await ExecuteLoadExpensesAsync());

        public DelegateCommand AddExpenseCommand => new DelegateCommand(async () => await AddExpenseAsync());

        public DelegateCommand<Expense> DeleteExpenseCommand => new DelegateCommand<Expense>(async (exp) => await DeleteExpenseAsync(exp));

        public DelegateCommand LogoffCommand => new DelegateCommand(async () => await ExecuteLogoffAsync());
        

        private readonly IExpenseTrackerService _expenseTrackerService;
        private readonly INavigationService _navigationService;
        private readonly IFirebaseService _firebaseService;

        public ExpenseListPageViewModel(IExpenseTrackerService expenseTrackerService, INavigationService navigationService, IFirebaseService firebaseService)
        {
            _expenseTrackerService = expenseTrackerService;
            _navigationService = navigationService;
            _firebaseService = firebaseService;

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

        private async Task DeleteExpenseAsync(Expense exp)
        {
            if (await _expenseTrackerService.DeleteExpenseAsync(exp))
            {
                await ExecuteLoadExpensesAsync();
            }
            else
            {
                await base.ShowErrorMessageAsync("Error deleting Expense on server.");
            }
        }

        

        private async Task ExecuteLogoffAsync()
        {
            _firebaseService.Logout();

            await _navigationService.NavigateAsync($"ExpenseTrackerApp:///{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }
    }
}

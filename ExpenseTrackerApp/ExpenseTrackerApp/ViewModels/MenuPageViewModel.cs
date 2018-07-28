using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackerApp.ViewModels
{
    public class MenuPageViewModel : BaseViewModel, INavigatingAware
    {
        public ObservableCollection<Models.MenuItem> MenuItems { get; set; }

        public Command<object> SelectMenuItemCommand => new Command<object>(async (obj) => await ExecuteSelectMenuItemAsync(obj));



        private readonly INavigationService _navigationService;

        public MenuPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            MenuItems = new ObservableCollection<Models.MenuItem>();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            MenuItems.Clear();

            MenuItems.Add(new Models.MenuItem { MenuTitle = "Expenses", Page = "ExpenseListPage" /*, Icon ="expenses.png" */});
            
            MenuItems.Add(new Models.MenuItem { MenuTitle = "Settings", Page = "SettingsPage" /*, Icon ="prefs.png" */});
        }



        private async Task ExecuteSelectMenuItemAsync(object selectedMenuItem)
        {
            Models.MenuItem menuItem = selectedMenuItem as Models.MenuItem;
            await _navigationService.NavigateAsync(nameof(NavigationPage) + "/" + menuItem.Page);
        }
    }
}

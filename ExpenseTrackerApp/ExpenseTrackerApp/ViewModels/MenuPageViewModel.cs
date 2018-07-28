using ExpenseTrackerApp.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace ExpenseTrackerApp.ViewModels
{
    public class MenuPageViewModel : BaseViewModel, INavigatingAware
    {
        public ObservableCollection<MenuItem> MenuItems { get; set; }


        public MenuPageViewModel()
        {
            MenuItems = new ObservableCollection<MenuItem>();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            MenuItems.Add(new MenuItem { MenuTitle = "Expenses", Page = "ExpenseListPage" /*, Icon ="expenses.png" */});
            
            MenuItems.Add(new MenuItem { MenuTitle = "Settings", Page = "SettingsPage" /*, Icon ="prefs.png" */});
        }
    }
}

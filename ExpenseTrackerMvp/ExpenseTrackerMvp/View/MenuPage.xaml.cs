
using System.Collections.Generic;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class MenuPage : ContentPage
    {

        List<MenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();


            ListViewMenu.ItemsSource = menuItems = new List<MenuItem>
                {
                    new MenuItem { MenuTitle = "Expenses", Page = new ExpensesPage() /*, Icon ="expenses.png" */},
                    new MenuItem { MenuTitle = "Categories", Page = new CategoriesPage() /*, Icon = "categories.png" */}
                };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (ListViewMenu.SelectedItem == null)
                    return;

                await App.NavigateMasterDetail(((MenuItem)e.SelectedItem).Page);
            };
            
        }

    }


    public class MenuItem
    {
        public string MenuTitle { get; set; }

        public string Icon { get; set; }               

        public ContentPage Page { get; set; }         
    }


}

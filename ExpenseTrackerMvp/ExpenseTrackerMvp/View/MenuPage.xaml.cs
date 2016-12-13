
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();


            btnExpenses.Clicked += async (sender, e) =>
            {
                await App.NavigateMasterDetail(new ExpensesPage());
            };


            btnCategories.Clicked += async (sender, e) =>
            {
                // TODO create CategoriesPage
                await App.NavigateMasterDetail(new LoginPage());
                                
            };

        }



    }
}

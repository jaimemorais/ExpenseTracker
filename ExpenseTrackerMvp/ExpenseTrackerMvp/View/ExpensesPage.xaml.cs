
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class ExpensesPage : ContentPage
    {
        public ExpensesPage()
        {
            InitializeComponent();

            BindingContext = new ViewModel.ExpenseViewModel();
        }
    }
}

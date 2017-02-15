
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class ExpensesCreatePage : ContentPage
    {
        public ExpensesCreatePage()
        {
            InitializeComponent();

            BindingContext = new ViewModel.ExpenseViewModel();
            
        }
    }
}

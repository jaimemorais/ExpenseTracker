
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class ExpensesPage : ContentPage
    {
        public ExpensesPage()
        {
            InitializeComponent();


            ViewModel.ExpenseViewModel expVM = new ViewModel.ExpenseViewModel();

            BindingContext = expVM;

                        
        }

                
    }
}

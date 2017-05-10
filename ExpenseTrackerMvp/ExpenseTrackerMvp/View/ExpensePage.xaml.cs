
using ExpenseTrackerMvp.Service;
using ExpenseTrackerMvp.ViewModel;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class ExpensePage : ContentPage
    {
        public ExpensePage()
        {
            InitializeComponent();


            ViewModel.ExpenseViewModel expVM = new ViewModel.ExpenseViewModel(new ExpenseTrackerWebApiClientService());

            BindingContext = expVM;

                        
        }

        protected override void OnAppearing()
        {
            ((ExpenseViewModel)BindingContext).LoadExpensesCommand.Execute(null);

            base.OnAppearing();
        }
        
        
    }
}

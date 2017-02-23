
using ExpenseTrackerMvp.ViewModel;
using System;
using System.Threading.Tasks;
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

        private async void ListViewExpenses_OnRefreshing(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                ((ExpenseViewModel)BindingContext).LoadCommand.Execute(null);
            });

            lvExpenses.EndRefresh();
        }

        
    }
}

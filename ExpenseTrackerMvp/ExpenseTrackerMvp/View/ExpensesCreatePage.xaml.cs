
using System;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class ExpensesCreatePage : ContentPage
    {
        public ExpensesCreatePage()
        {
            InitializeComponent();

            BindingContext = new ViewModel.ExpenseViewModel();


            this.EntryDate.Date = DateTime.Now.Date;
            this.EntryValue.Text = string.Empty;
        }
    }
}

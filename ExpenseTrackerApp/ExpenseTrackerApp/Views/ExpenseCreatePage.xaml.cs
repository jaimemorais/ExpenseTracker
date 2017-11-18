using Xamarin.Forms;

namespace ExpenseTrackerApp.Views
{
    public partial class ExpenseCreatePage : ContentPage
    {
        public ExpenseCreatePage()
        {
            InitializeComponent();
        }

        private void pckCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            EntryValue.Focus();
        }

        private void pckPaymentType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            EntryDescription.Focus();
        }
    }
}

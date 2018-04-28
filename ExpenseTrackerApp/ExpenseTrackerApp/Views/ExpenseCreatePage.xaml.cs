using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackerApp.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
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
            //EntryDescription.Focus();
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            this.EntryValue.Text = string.Empty;            
        }
        
        private void EntryValue_Completed(object sender, System.EventArgs e)
        {
            // TODO remove
            if (this.EntryValue.Text == "4.05")
            {
                string onibus = "Onibus";
                if (this.pckCategory.Items.Any(c => c == onibus))
                    this.pckCategory.SelectedItem = onibus;

                string carteira = "Carteira";
                if (this.pckPaymentType.Items.Any(p => p == carteira))
                    this.pckPaymentType.SelectedItem = carteira;

                this.EntryValue.Unfocus();
            }
        }
    }
}

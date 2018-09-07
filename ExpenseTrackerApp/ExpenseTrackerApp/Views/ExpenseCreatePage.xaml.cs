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
            if (pckCategory != null && pckCategory.SelectedItem != null &&
                ((string)pckCategory.SelectedItem).Equals("Onibus"))
            {
                EntryValue.Text = "4.05";

                string carteira = "Carteira";
                if (this.pckPaymentType.Items.Any(p => p == carteira))
                    this.pckPaymentType.SelectedItem = carteira;                                
            }
            else
            {
                EntryValue.Text = string.Empty;
                EntryValue.Focus();
            }
        }


        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            EntryValue.Text = string.Empty;            
        }
        
    }
}

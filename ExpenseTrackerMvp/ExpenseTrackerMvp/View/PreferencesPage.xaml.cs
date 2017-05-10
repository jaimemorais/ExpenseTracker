
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class PreferencesPage : ContentPage
    {
        public PreferencesPage()
        {
            InitializeComponent();

            BindingContext = new ExpenseTrackerMvp.ViewModel.PreferencesViewModel();
        }
    }
}

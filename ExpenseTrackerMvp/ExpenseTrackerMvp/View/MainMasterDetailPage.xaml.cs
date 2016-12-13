
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        public MainMasterDetailPage()
        {
            InitializeComponent();

            this.Master = new MenuPage();
            this.Detail = new NavigationPage(new ExpensesPage());

            App.ExpenseTrackerMasterDetailPage = this;            
        }
    }
}

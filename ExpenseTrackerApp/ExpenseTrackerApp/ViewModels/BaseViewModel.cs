using Prism.Mvvm;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.ViewModels
{
    public class BaseViewModel : BindableBase
    {

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }



        protected async Task ShowErrorMessageAsync(string msg)
        {
            await App.Current.MainPage.DisplayAlert("ExpenseTracker Error", msg, "OK");
        }

    }
}

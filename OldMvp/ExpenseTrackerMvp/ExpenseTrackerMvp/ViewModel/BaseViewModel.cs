using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        private bool _busy;
        public bool IsBusy
        {
            get
            {
                return _busy;
            }
            set
            {
                _busy = value;
                this.Notify();
            }
        }



        protected async Task ShowErrorMessage(string msg)
        {
            await App.Current.MainPage.DisplayAlert("ExpenseTracker Error", msg, "OK");
        }

    }
}

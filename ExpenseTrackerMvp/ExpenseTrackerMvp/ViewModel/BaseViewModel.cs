using ExpenseTrackerMvp.Util;
using System;
using System.ComponentModel;
using System.Net.Http;
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
                
        protected string GetApiServiceURL(string apiId)
        {
            try
            {
                // AVD
                // 10.0.2.2 = localhost - emulator
                // IIS, not iisexpress 

                // Hyper v 
                // http://briannoyesblog.azurewebsites.net/2016/03/06/calling-localhost-web-apis-from-visual-studio-android-emulator/
                // enable firewall rule for port 80
                // http://169.254.80.80  (Desktop Adapter #2 )

                return AppConfig.Instance.GetExpenseTrackerApiUrl() + apiId;
            }
            catch
            {
                throw new Exception("expensetracker-api-url not set.");
            }
        }


        protected HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            httpClient.Timeout = new System.TimeSpan(0, 0, 3);
            return httpClient;
        }


        protected async Task ShowErrorMessage(string msg)
        {
            await App.Current.MainPage.DisplayAlert("ExpenseTracker Error", msg, "OK");
        }

    }
}

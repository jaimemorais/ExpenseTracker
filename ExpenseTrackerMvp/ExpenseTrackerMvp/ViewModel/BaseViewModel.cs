using ExpenseTrackerMvp.Util;
using System;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.ViewModel
{
    public class BaseViewModel
    {
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

        protected async Task ShowErrorMessage(string msg)
        {
            await App.Current.MainPage.DisplayAlert("Error", msg, "OK");
        }

    }
}

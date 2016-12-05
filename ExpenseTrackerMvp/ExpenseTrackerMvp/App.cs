
using ExpenseTrackerMvp.Util;
using Xamarin.Forms;

namespace ExpenseTrackerMvp
{
    public class App : Application
    {
        public App()
        {
            var firebaseAuthToken = UserSettings.GetFirebaseAuthToken();

            if (firebaseAuthToken != null)
            {
                MainPage = new View.ExpensesPage();
            }
            else
            {
                MainPage = new View.LoginPage();
            }
                        
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
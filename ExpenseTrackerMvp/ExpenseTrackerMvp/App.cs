
using Xamarin.Forms;

namespace ExpenseTrackerMvp
{
    public class App : Application
    {
        public App()
        {
            // TODO if logged, go to ExpenseView else to Login            
                        
            MainPage = new View.LoginPage();
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

using ExpenseTrackerMvp.Util;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackerMvp
{
    public class App : Application
    {
        public static MasterDetailPage ExpenseTrackerMasterDetailPage { get; set; }

        public async static Task NavigateMasterDetail(Page page)
        {
            // reference :  https://www.youtube.com/watch?v=UBqdI77_p-M&feature=youtu.be

            // Hide the menu when navigate
            App.ExpenseTrackerMasterDetailPage.IsPresented = false;

            // Navigate to page
            await App.ExpenseTrackerMasterDetailPage.Detail.Navigation.PushAsync(page);
        }

        public App()
        {
        }

        private void CheckAuthToken()
        {
            var firebaseAuthToken = UserSettings.GetFirebaseAuthToken();

            var authOK = false; // TODO FirebaseService.CheckAuthWithCurrentToken(firebaseAuthToken);

            if (authOK && firebaseAuthToken != null)
            {
                MainPage = new View.MainMasterDetailPage();
            }
            else
            {
                MainPage = new View.LoginPage();
            }
        }

        protected override void OnStart()
        {            
            CheckAuthToken();
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
using ExpenseTrackerMvp.Service;
using ExpenseTrackerMvp.Util;
using ExpenseTrackerMvp.View;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
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

        public async static Task NavigateMasterDetailModal(Page page)
        {         
            // Navigate to page
            await App.ExpenseTrackerMasterDetailPage.Detail.Navigation.PushModalAsync(page);

        }

        public async static Task NavigateMasterDetailModalBack(string imageToShow)
        {

            // Show image after save expense
            if (imageToShow != null)
            {
                await App.ExpenseTrackerMasterDetailPage.Detail.Navigation.PushModalAsync(new ShowGifPage(imageToShow));
                await Task.Delay(1500);
                await App.ExpenseTrackerMasterDetailPage.Detail.Navigation.PopModalAsync();                
            }

            await App.ExpenseTrackerMasterDetailPage.Detail.Navigation.PopModalAsync();
        }

        public App()
        {
        }
        
        protected override void OnStart()
        {            
            if (FirebaseService.Instance.LoginWithUserSettings(UserSettings.GetEmail(), UserSettings.GetPassword()))
            {
                MainPage = new View.MainMasterDetailPage();
            }
            else
            {
                MainPage = new View.LoginPage();
            }
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
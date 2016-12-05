using ExpenseTrackerMvp.Util;
using Firebase.Xamarin.Auth;
using System;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void OnLoginButtonClicked(object sender, EventArgs args)
        {
            string user = this.EntryUser.Text;
            string pass = this.EntryPass.Text;

            try
            {
                FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppConfig.Instance.GetFirebaseApiKey()));
                FirebaseAuthLink authLink = await authProvider.SignInWithEmailAndPasswordAsync(user, pass);
                
                if (authLink != null && authLink.FirebaseToken != null)
                {
                    UserSettings.SaveFirebaseAuthToken(authLink.FirebaseToken);

                    App.Current.MainPage = new ExpensesPage();
                }
                else
                {
                    await DisplayAlert("Error", "Username/password is incorrect.", "OK");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", "Error : " + e.Message, "OK");
            }
        }
        
    }
}

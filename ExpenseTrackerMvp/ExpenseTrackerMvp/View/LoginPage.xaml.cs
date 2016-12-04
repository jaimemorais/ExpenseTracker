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

            
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppConfig.Instance.GetFirebaseApiKey()));
            var authLink = await authProvider.SignInWithEmailAndPasswordAsync(user, pass);

            string firebaseToken = authLink.FirebaseToken;

            if (firebaseToken != null)
            {
                UserSettings.SaveFirebaseAuthToken(firebaseToken);

                await this.Navigation.PushAsync(new ExpensesPage());
            }
            else
            {
                await DisplayAlert("Error", "Username/password is incorrect.", "OK");
            }
            
        }
    }
}

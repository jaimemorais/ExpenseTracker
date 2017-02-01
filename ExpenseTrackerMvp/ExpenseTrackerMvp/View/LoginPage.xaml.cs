using ExpenseTrackerMvp.Service;
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
                await FirebaseService.Instance.LoginAsync(user, pass);
                                                
                if (FirebaseService.Instance.CurrentUser != null)
                {
                    App.Current.MainPage = new View.MainMasterDetailPage();
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

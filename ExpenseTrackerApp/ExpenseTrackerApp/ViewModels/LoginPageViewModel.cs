using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackerApp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        public DelegateCommand ExecuteLoginCommand => new DelegateCommand(async () => await ExecuteLoginAsync());




        private readonly IFirebaseService _firebaseService;
        private readonly INavigationService _navigationService;
        private readonly ITelemetry _telemetry;
        

        public LoginPageViewModel(IFirebaseService firebaseService, ITelemetry telemetry, INavigationService navigationService)
        {
            _firebaseService = firebaseService;
            _navigationService = navigationService;
            _telemetry = telemetry;
        }


        private async Task ExecuteLoginAsync()
        {
            try
            {
                await _firebaseService.LoginAsync(_email, _password);

                if (_firebaseService.GetCurrentUser() != null)
                {                    
                    await _navigationService.NavigateAsync($"ExpenseTrackerApp:///{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(ExpenseListPage)}");
                }
                else
                {
                    await ShowErrorMessageAsync("Email/Password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                _telemetry.LogError("ExecuteLoginAsync error.", ex);
                await ShowErrorMessageAsync("Login Error");
                return;
            }

        }

    }
}

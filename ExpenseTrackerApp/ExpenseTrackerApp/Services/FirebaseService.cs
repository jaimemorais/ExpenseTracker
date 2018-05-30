using ExpenseTrackerApp.Helpers;
using ExpenseTrackerApp.Settings;
using Firebase.Auth;
using Firebase.Auth.Payloads;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public class FirebaseService : IFirebaseService
    {

        private User _currentUser = null;

        public User GetCurrentUser() => _currentUser;


        private readonly IUserSettings _userSettings;
        private readonly ITelemetry _telemetry;

        public FirebaseService(IUserSettings userSettings, ITelemetry telemetry)
        {
            _userSettings = userSettings;
            _telemetry = telemetry;
        }


        public async Task LoginAsync(string email, string pwd)
        {
             await InternalLoginAsync(email, pwd);

            _userSettings.SaveEmail(email);
            await _userSettings.SavePasswordAsync(pwd);
        }

        public async Task LoginWithUserSettingsAsync(string email, string pwd)
        {
            await InternalLoginAsync(email, pwd);
        }

        private async Task InternalLoginAsync(string email, string pwd)
        {
            var authOptions = new FirebaseAuthOptions(Secrets.FIREBASE_API_KEY);
            var firebaseAuthService = new FirebaseAuthService(authOptions);
            
            var request = new VerifyPasswordRequest()
            {
                Email = email,
                Password = pwd
            };

            try
            {
                VerifyPasswordResponse response = await firebaseAuthService.VerifyPassword(request);

                _currentUser = new User
                {
                    Email = response.Email,
                    Token = response.IdToken
                };

            }
            catch (FirebaseAuthException e)
            {
                _telemetry.LogError("Error in firebase login", e);                
            }
        }

        

        public void Logout()
        {
            _userSettings.SaveEmail(string.Empty);
            _userSettings.SavePasswordAsync(string.Empty);
            _currentUser = null;
        }
    }
}

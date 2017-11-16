using ExpenseTrackerApp.Settings;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public class FirebaseService : IFirebaseService
    {

        private User _currentUser = null;

        public User GetCurrentUser() => _currentUser;


        private readonly IUserSettings _userSettings;

        public FirebaseService(IUserSettings userSettings)
        {
            _userSettings = userSettings;
        }


        public async Task LoginAsync(string email, string pwd)
        {
            _currentUser = null;

            FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppSettings.FIREBASE_API_KEY));
            FirebaseAuthLink authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, pwd);
            
            _currentUser = authLink.User;

            _userSettings.SaveEmail(email);
            _userSettings.SavePassword(pwd);          
        }

        public FirebaseClient GetFirebaseExpenseTrackerClient()
        {
            return new FirebaseClient(AppSettings.FIREBASE_URL);
        }


        public bool LoginWithUserSettings(string email, string pwd)
        {
            try
            {
                FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppSettings.FIREBASE_API_KEY));
                FirebaseAuthLink authLink = authProvider.SignInWithEmailAndPasswordAsync(email, pwd).Result;

                _currentUser = authLink.User;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Logout()
        {
            _userSettings.SaveEmail(string.Empty);
            _userSettings.SavePassword(string.Empty);
            _currentUser = null;
        }
    }
}

using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.Util
{
    public class FirebaseService
    {
        public static FirebaseService Instance { get; } = new FirebaseService();

        private User _currentUser = null;
        public User CurrentUser => _currentUser;



        public async Task LoginAsync(string email, string pwd)
        {
            _currentUser = null;

            try
            {
                FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppConfig.Instance.GetFirebaseApiKey()));
                FirebaseAuthLink authLink = await authProvider.SignInWithEmailAndPasswordAsync(email, pwd);

                _currentUser = authLink.User;

                UserSettings.SaveEmail(email);
                UserSettings.SavePassword(pwd);
            }
            catch
            {
                return;
            }
        }

        public FirebaseClient GetFirebaseExpenseTrackerClient()
        {
            return new FirebaseClient("https://expensetrackermvp.firebaseio.com/");
        }


        public bool LoginWithUserSettingsAsync(string email, string pwd)
        {            
            try
            { 
                FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppConfig.Instance.GetFirebaseApiKey()));
                FirebaseAuthLink authLink = authProvider.SignInWithEmailAndPasswordAsync(email, pwd).Result;

                _currentUser = authLink.User;

                return true;
            }
            catch
            {
                return false;
            }  
        }

    }
}

using ExpenseTrackerMvp.Util;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using System;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.Service
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

                await UserSettings.Instance.SaveEmailAsync(email);
                await UserSettings.Instance.SavePasswordAsync(pwd);
            }
            catch (Exception ex)
            {
                // TODO logging service
                return;
            }
        }

        public FirebaseClient GetFirebaseExpenseTrackerClient()
        {
            return new FirebaseClient("https://expensetrackermvp.firebaseio.com/");
        }


        public bool LoginWithUserSettings(string email, string pwd)
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

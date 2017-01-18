using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using System.Threading.Tasks;

namespace ExpenseTrackerMvp.Util
{
    public class FirebaseService
    {
        public static async Task<string> SignInAndGetFirebaseToken(string user, string pass)
        {
            FirebaseAuthProvider authProvider = new FirebaseAuthProvider(new FirebaseConfig(AppConfig.Instance.GetFirebaseApiKey()));
            FirebaseAuthLink authLink = await authProvider.SignInWithEmailAndPasswordAsync(user, pass);

            return authLink.FirebaseToken;
        }

        public static FirebaseClient GetFirebaseExpenseTrackerClient()
        {
            return new FirebaseClient("https://expensetrackermvp.firebaseio.com/");
        }


        public static bool CheckAuthWithCurrentToken(string firebaseAuthTokenFromUserSettings)
        {
            // TODO
            return false;            
        }

    }
}

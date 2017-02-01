using Xamarin.Forms;

namespace ExpenseTrackerMvp.Util
{
    public static class UserSettings
    {       

        public static void SaveFirebaseAuthToken(string authToken)
        {   
            Application.Current.Properties["FirebaseAuthToken"] = authToken;
        }
        
        public static string GetFirebaseAuthToken()
        {
            if (Application.Current.Properties.ContainsKey("FirebaseAuthToken"))
            {
                return Application.Current.Properties["FirebaseAuthToken"].ToString();                
            }

            return null;       
        }
    }
}

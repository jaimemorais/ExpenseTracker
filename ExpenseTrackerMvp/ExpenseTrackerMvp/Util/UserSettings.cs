using Xamarin.Forms;

namespace ExpenseTrackerMvp.Util
{
    public static class UserSettings
    {       
        private static string EMAIL = "email";
        private static string PWD = "pwd";

        public static void SaveEmail(string email)
        {   
            Application.Current.Properties[EMAIL] = email;
        }

        public static void SavePassword(string pwd)
        {
            Application.Current.Properties[PWD] = EncryptString(pwd);
        }

        public static string GetEmail()
        {
            if (Application.Current.Properties.ContainsKey(EMAIL))
            {
                return Application.Current.Properties[EMAIL].ToString();                
            }

            return null;       
        }

        public static string GetPassword()
        {
            if (Application.Current.Properties.ContainsKey(PWD))
            {
                return DecryptString(Application.Current.Properties[PWD].ToString());
            }

            return null;
        }





        private static string EncryptString(string str)
        {
            // TODO
            return null;
        }

        private static string DecryptString(string str)
        {
            // TODO
            return null;
        }
    }
}

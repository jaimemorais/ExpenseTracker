using ExpenseTrackerApp.Utils;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ExpenseTrackerApp.Settings
{
    public class UserSettings : IUserSettings
    {
        private static string EMAIL = "email";
        private static string PWD = "pwd";
        private static string SHOW_PUPPY = "show_puppy";


        private static ISettings Settings
        {
            get
            {
                return CrossSettings.Current;
            }
        }




        public void SaveEmail(string email)
        {
            Settings.AddOrUpdateValue(EMAIL, email);
        }

        public void SavePassword(string pwd)
        {
            Settings.AddOrUpdateValue(PWD, CryptoUtils.EncryptStr(pwd));
        }



        public string GetEmail()
        {
            return Settings.GetValueOrDefault(EMAIL, "patricia@exptracker.com"); // TODO string.Empty);
        }

        public string GetPassword()
        {
            try
            {
                return CryptoUtils.DecryptStr(Settings.GetValueOrDefault(PWD, string.Empty));
            }
            catch
            {
                return null;
            }
        }


        public void SaveShowPuppyPref(bool value)
        {
            Settings.AddOrUpdateValue(SHOW_PUPPY, value);
        }
        
        public bool GetShowPuppyPref()
        {
            return Settings.GetValueOrDefault(SHOW_PUPPY, true);
        }


    }
}

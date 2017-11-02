using ExpenseTrackerMvp.Service;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.Util
{
    public class UserSettings
    {
        public static UserSettings Instance { get; } = new UserSettings();

        private static string EMAIL = "email";
        private static string PWD = "pwd";
        private static string SHOW_PUPPY = "show_puppy";

        public async Task SaveEmailAsync(string email)
        {   
            Application.Current.Properties[EMAIL] = email;
            await Application.Current.SavePropertiesAsync();
        }

        public async Task SavePasswordAsync(string pwd)
        {
            Application.Current.Properties[PWD] = 
                DependencyService.Get<ICryptoService>().EncryptString(pwd, AppConfig.Instance.GetExpenseTrackerCryptoPassword());
            await Application.Current.SavePropertiesAsync();
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
            try
            {
                if (Application.Current.Properties.ContainsKey(PWD))
                {
                    return DependencyService.Get<ICryptoService>().DecryptString(Application.Current.Properties[PWD].ToString(), AppConfig.Instance.GetExpenseTrackerCryptoPassword());
                }
            }
            catch
            {
                return null;
            }

            return null;
        }


        public async Task SaveShowPuppyPrefAsync(bool value)
        {
            Application.Current.Properties[SHOW_PUPPY] = value;
            await Application.Current.SavePropertiesAsync();
        }
        
        public static bool GetShowPuppyPref()
        {
            if (Application.Current.Properties.ContainsKey(SHOW_PUPPY))
            {
                return (bool)Application.Current.Properties[SHOW_PUPPY];
            }

            return true;
        }


    }
}

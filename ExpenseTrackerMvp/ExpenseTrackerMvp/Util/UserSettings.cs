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
            if (Application.Current.Properties.ContainsKey(PWD))
            {
                return DependencyService.Get<ICryptoService>().DecryptString(Application.Current.Properties[PWD].ToString(), AppConfig.Instance.GetExpenseTrackerCryptoPassword());
            }

            return null;
        }
        




    }
}

using Android.App;
using Android.Content;
using Android.Views.InputMethods;
using ExpenseTrackerApp.Droid.Helpers;
using ExpenseTrackerApp.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(KeyboardHelper))]
namespace ExpenseTrackerApp.Droid.Helpers
{
    public class KeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {                        
            var context = MainActivity.Instance;

            if (context.GetSystemService(Context.InputMethodService) is InputMethodManager inputMethodManager)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}
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
            var context = Forms.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}
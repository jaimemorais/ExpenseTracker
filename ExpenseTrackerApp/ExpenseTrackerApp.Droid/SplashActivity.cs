
using Android.App;
using Android.OS;

namespace ExpenseTrackerApp.Droid
{
    [Activity(Theme = "@style/Theme.Splash", //Indicates the theme to use for this activity
             MainLauncher = true, //Set it as boot activity
             NoHistory = true)] //Doesn't place it in back stack
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {

            // TODO 
            // set theme splash or splash 3
            // SetTheme(Theme. "Theme.Splash");


            base.OnCreate(bundle);
            
            System.Threading.Thread.Sleep(300); 

            this.StartActivity(typeof(MainActivity));
        }
    }
}


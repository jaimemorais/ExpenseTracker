using Android.App;
using Android.OS;

namespace ExpenseTrackerApp.Droid
{
    [Activity(Theme = "@style/Theme.SplashCat", //Indicates the theme to use for this activity
             MainLauncher = true, //Set it as boot activity
             NoHistory = true)] //Doesn't place it in back stack
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {            
            SetTheme(System.DateTime.Now.Second % 2 == 0 ? Resource.Style.Theme_SplashCat : Resource.Style.Theme_SplashDog);
         
            base.OnCreate(bundle);
            
            System.Threading.Thread.Sleep(300); 

            this.StartActivity(typeof(MainActivity));
        }
    }
}


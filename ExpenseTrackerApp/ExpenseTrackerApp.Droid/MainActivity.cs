using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using ExpenseTrackerApp.Droid.CustomRenderers;
using Prism;
using Prism.Ioc;
using System;

namespace ExpenseTrackerApp.Droid
{
    [Activity(Label = "Expense Tracker", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        internal static MainActivity Instance { get; private set; }



        public event EventHandler<ActivityResultEventArgs> ActivityResult = delegate { };


        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            ActivityResult(this, new ActivityResultEventArgs
            {
                RequestCode = requestCode,
                ResultCode = resultCode,
                Data = data
            });
        }

    }

    public class AndroidInitializer : IPlatformInitializer
    {
        
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        
        }
    }
}


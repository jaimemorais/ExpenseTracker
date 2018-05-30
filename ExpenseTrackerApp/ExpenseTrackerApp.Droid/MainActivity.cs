using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
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



        public event EventHandler<VoiceActivityResultEventArgs> VoiceActivityResult = delegate { };


        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;

            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);


            Xamarin.Essentials.Platform.Init(this, bundle);


            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            VoiceActivityResult(this, new VoiceActivityResultEventArgs
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


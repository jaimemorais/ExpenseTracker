using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;
using System;

namespace ExpenseTrackerApp.Droid
{

    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);




            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {                
                FirebasePushNotificationManager.DefaultNotificationChannelId = "EspTrackerChannelId";                
                FirebasePushNotificationManager.DefaultNotificationChannelName = "EspTrackerChannel";
            }


            FirebasePushNotificationManager.Initialize(this, false);

        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}
using Android.App;
using Android.Content;
using System;

namespace ExpenseTrackerApp.Droid.CustomRenderers
{
    public class ActivityResultEventArgs : EventArgs
    {
        public int RequestCode { get; set; }
        public Result ResultCode { get; set; }
        public Intent Data { get; set; }

        public ActivityResultEventArgs() : base()
        { }
    }
}
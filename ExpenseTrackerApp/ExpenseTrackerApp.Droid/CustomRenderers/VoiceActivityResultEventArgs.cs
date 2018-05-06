using Android.App;
using Android.Content;
using System;

namespace ExpenseTrackerApp.Droid.CustomRenderers
{
    public class VoiceActivityResultEventArgs : EventArgs
    {
        public int RequestCode { get; set; }
        public Result ResultCode { get; set; }
        public Intent Data { get; set; }

        public VoiceActivityResultEventArgs() : base()
        { }
    }
}
using System;
using Xamarin.Forms;

namespace ExpenseTrackerApp.CustomRenderers
{
    public class VoiceButton : Button
    {
        public Action<string> OnTextChanged { get; set; }
    }

}

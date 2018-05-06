using ExpenseTrackerApp.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackerApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseListPage : ContentPage
    {

        static ISpeechToText speechRecognitionInstance;

        public ExpenseListPage()
        {
            InitializeComponent();
                        
            voiceButton.OnTextChanged += (s) => {
                LabelVoiceText.Text = s;
            };

        }

        public void OnStart(Object sender, EventArgs args)
        {
            speechRecognitionInstance.Start();
        }
        public void OnStop(Object sender, EventArgs args)
        {
            speechRecognitionInstance.Stop();
        }

    }
}

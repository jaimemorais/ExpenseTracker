using ExpenseTrackerMvp.Util;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.ViewModel
{
    public class PreferencesViewModel : BaseViewModel
    {

        private bool _showPuppyPref;

        public bool ShowPuppyPref
        {
            get { return _showPuppyPref; }
            set { _showPuppyPref = value; }
        }



        public PreferencesViewModel()
        {
            SavePreferencesCommand = new Command(ExecuteSavePreferences);

            ShowPuppyPref = UserSettings.GetShowPuppyPref();
        }

        public Command SavePreferencesCommand { get; }
        




        private async void ExecuteSavePreferences()
        {
            await UserSettings.Instance.SaveShowPuppyPrefAsync(_showPuppyPref);
        }

        

    }
}
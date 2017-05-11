using ExpenseTrackerMvp.Util;

namespace ExpenseTrackerMvp.ViewModel
{
    public class PreferencesViewModel : BaseViewModel
    {

        private bool _showPuppyPref;

        public bool ShowPuppyPref
        {
            get { return _showPuppyPref; }
            set
            {
                _showPuppyPref = value;
                ExecuteSavePreferences();
            }
        }



        public PreferencesViewModel()
        {
            _showPuppyPref = UserSettings.GetShowPuppyPref();
        }
        

        private async void ExecuteSavePreferences()
        {
            await UserSettings.Instance.SaveShowPuppyPrefAsync(_showPuppyPref);
        }

        

    }
}
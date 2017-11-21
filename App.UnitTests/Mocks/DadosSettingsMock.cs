using ExpenseTrackerApp.Settings;

namespace App.UnitTests
{
    internal class DadosSettingsMock : IUserSettings
    {
        public string GetEmail()
        {
            throw new System.NotImplementedException();
        }

        public string GetPassword()
        {
            throw new System.NotImplementedException();
        }

        public bool GetShowPuppyPref()
        {
            throw new System.NotImplementedException();
        }

        public void SaveEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public void SavePassword(string pwd)
        {
            throw new System.NotImplementedException();
        }

        public void SaveShowPuppyPref(bool value)
        {
            throw new System.NotImplementedException();
        }
    }
}
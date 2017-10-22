namespace ExpenseTrackerApp.Settings
{
    public interface IUserSettings
    {

        void SaveEmail(string email);
        void SavePassword(string pwd);
        string GetEmail();
        string GetPassword();
        void SaveShowPuppyPref(bool value);
        bool GetShowPuppyPref();

    }
}

using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public interface IFirebaseService
    {
        Task LoginAsync(string email, string pwd);

        Task LoginWithUserSettingsAsync(string email, string pwd);

        User GetCurrentUser();
        
        void Logout();
    }

}
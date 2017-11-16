using Firebase.Xamarin.Auth;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public interface IFirebaseService
    {
        Task LoginAsync(string email, string pwd);

        bool LoginWithUserSettings(string email, string pwd);

        User GetCurrentUser();
        
        void Logout();
    }

}
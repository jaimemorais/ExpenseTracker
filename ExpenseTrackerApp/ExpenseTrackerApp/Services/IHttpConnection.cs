using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public interface IHttpConnection
    {
        Task<T> GetAsync<T>(string uri);

        Task<bool> PostAsync<T>(string uri, object objPost);

        Task<bool> DeleteAsync(string uri);
    }
}

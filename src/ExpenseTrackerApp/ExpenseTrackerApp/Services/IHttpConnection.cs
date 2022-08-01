using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public interface IHttpConnection
    {
        Task<T> GetAsync<T>(string uri);

        Task<bool> PostJsonAsync<T>(string uri, object objPost);

        Task<bool> PostStringContentAsync<T>(string uri, string content);

        Task<bool> DeleteAsync(string uri);
    }
}

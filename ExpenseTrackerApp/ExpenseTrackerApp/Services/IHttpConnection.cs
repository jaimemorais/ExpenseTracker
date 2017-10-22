using System.Threading.Tasks;

namespace ExpenseTrackerApp.Services
{
    public interface IHttpConnection
    {
        Task<T> GetAsync<T>(string uri);

        Task<T> PostAsync<T>(string uri, object objPost);

    }
}

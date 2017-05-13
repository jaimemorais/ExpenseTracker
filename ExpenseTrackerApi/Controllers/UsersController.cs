using ExpenseTrackerDomain.Models;
using ExpenseTrackerWeb.Helpers;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpenseTrackerApi.Controllers.RestApi
{

    public class UsersController : ApiController
    {
        private void CheckAuth()
        {
            // TODO auth - meanwhile I use this
            if (UtilApi.GetHeaderValue(Request, "ApiToken") != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }


        // GET api/Users
        public async Task<IEnumerable<string>> GetAsync()
        {
            CheckAuth();

            MongoHelper<User> userHelper = new MongoHelper<User>();
            
            IList<string> returnList = new List<string>();
            await userHelper.Collection.Find(u => u.UserName != null)
                .ForEachAsync(userDocument =>
                {
                    string docJson = Newtonsoft.Json.JsonConvert.SerializeObject(userDocument);
                    returnList.Add(docJson);
                }
            );

            return returnList.ToArray();
        }
    }
}

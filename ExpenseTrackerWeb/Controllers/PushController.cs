using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpenseTrackerWebApi.Controllers
{
    public class PushController : BaseApiController
    {
                
        [HttpPost]
        public async Task UpdateUserFcmToken(string fcmToken)
        {
            CheckAuth();
            
            try
            {
                string userName = UtilApi.GetHeaderValue(Request, "CurrentUserName");

                var filter = Builders<User>.Filter.Eq(u => u.UserName, userName);
                var update = Builders<User>.Update.Set("FirebaseCloudMessagingToken", fcmToken);                                                      

                MongoHelper<User> userHelper = new MongoHelper<User>();
                await userHelper.Collection.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {
                Trace.TraceError("UpdateUserFcmToken error : " + e.Message);
                throw;
            }
        }

        
        [HttpGet]
        public async Task SendPushNotificationAsync(string userName, string title, string body)
        {
            CheckAuth();

            try
            {
                MongoHelper<User> userHelper = new MongoHelper<User>();
                User user = await userHelper.Collection.Find(u => u.UserName.Equals(userName)).FirstAsync();
                string userFcmToken = user.FirebaseCloudMessagingToken;

                string jsonMessage =
                    "{\"to\" : \"" + userFcmToken + "\", " +
                    " \"data\": {\"title\": \" " + title + "\", \"body\": \"" + body + "\"}" +
                    "}";


                var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
                request.Headers.TryAddWithoutValidation("Authorization", "key=" + ConfigurationManager.AppSettings.Get("FIREBASE_CLOUD_MESSAGING_KEY"));
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("SendPushNotificationAsync error : " + e.Message);
                throw;
            }
        }
    }
}

using ExpenseTrackerDomain.Models;
using ExpenseTrackerWebApi.Helpers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpenseTrackerWebApi.Controllers
{
    public class PushController : BaseApiController
    {
                
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateUserFcmToken()
        {
            CheckAuth();
            
            try
            {
                string userName = ApiUtils.GetHeaderValue(Request, "CurrentUserName");

                HttpContent requestContent = Request.Content;
                string fcmTokenReceived = requestContent.ReadAsStringAsync().Result;

                var filter = Builders<User>.Filter.Eq(u => u.UserName, userName);
                var update = Builders<User>.Update.Set("FirebaseCloudMessagingToken", fcmTokenReceived);                                                      

                MongoHelper<User> userHelper = new MongoHelper<User>();
                UpdateResult updateResult = await userHelper.Collection.UpdateOneAsync(filter, update);

                if (updateResult.IsAcknowledged)
                {
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.OK };
                }
                else
                {
                    return new HttpResponseMessage() { StatusCode = HttpStatusCode.NotFound };
                }
            }
            catch (Exception e)
            {
                Trace.TraceError("UpdateUserFcmToken error : " + e.Message);
                return new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError , Content = new StringContent(e.Message) };
            }
        }


        // TODO change to http POST (remember to change the cronjob also - https://cron-job.org)
        [HttpGet]
        public async Task<string> SendPushNotificationAsync(string apiToken, string userName, string title, string body)
        {
            if (apiToken != ConfigurationManager.AppSettings.Get("expensetracker-api-token"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

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

                    if (result.IsSuccessStatusCode)
                    {
                        return "Push sent OK.";
                    }
                    else
                    {
                        return "Error sending Push. Firebase Cloud Messaging HttpStatusCode : " + (int)result.StatusCode;
                    }                    
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

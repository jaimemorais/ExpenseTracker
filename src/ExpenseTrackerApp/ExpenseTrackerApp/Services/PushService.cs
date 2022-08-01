using ExpenseTrackerApp.Service;
using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTrackerApp.Services
{
    public class PushService : IPushService
    {

        private readonly ITelemetry _telemetry;
        private readonly IExpenseTrackerService _expenseTrackerService;

        public PushService(ITelemetry telemetry, IExpenseTrackerService expenseTrackerService)
        {
            _telemetry = telemetry;
            _expenseTrackerService = expenseTrackerService;
        }



        public void Initialize()
        {            
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                try
                {
                    string newToken = CrossFirebasePushNotification.Current.Token;
                    _expenseTrackerService.UpdateUserFCMToken(newToken);
                }
                catch (Exception ex)
                {
                    _telemetry.LogError("Error PushService OnTokenRefresh", ex);
                }                
            };
            

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                
            };

            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {                

            };

            CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            {
                
            };

        }


        


    }
}

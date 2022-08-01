using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;

namespace ExpenseTrackerApp.Services
{
    public class Telemetry : ITelemetry
    {
        public void LogError(string errorMessage, Exception ex = null)
        {
            string logEx = string.Empty;
            if (ex != null)
            {
                logEx += ex.Message + (ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                logEx += ex.StackTrace != null ? "  StackTrace : " + ex.StackTrace : string.Empty;
            }

            var props = new Dictionary<string, string>
            {
                { "Exception Log", logEx }
            };

            Analytics.TrackEvent("ERROR - " + errorMessage, props);
        }

        public void LogEvent(string eventMessage, IDictionary<string, string> props = null)
        {
            Analytics.TrackEvent(eventMessage, props);
        }
        
    }
}

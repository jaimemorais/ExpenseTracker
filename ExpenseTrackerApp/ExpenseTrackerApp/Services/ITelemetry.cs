using System;
using System.Collections.Generic;

namespace ExpenseTrackerApp.Services
{
    public interface ITelemetry
    {
        void LogError(string errorMessage, Exception ex = null);

        void LogEvent(string eventMessage, IDictionary<string, string> props = null);
    }
}

using ExpenseTrackerApp.Services;
using System;
using System.Collections.Generic;

namespace App.UnitTests
{
    internal class TelemetryMock : ITelemetry
    {
        public void LogError(string errorMessage, Exception ex = null)
        {
            // TODO
        }

        public void LogEvent(string eventMessage, IDictionary<string, string> props = null)
        {
            // TODO
        }
    }
}
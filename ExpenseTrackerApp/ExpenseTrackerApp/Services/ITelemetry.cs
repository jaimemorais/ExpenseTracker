using System;

namespace ExpenseTrackerApp.Services
{
    public interface ITelemetry
    {
        void LogError(string errorMessage, Exception ex = null);
        
    }
}

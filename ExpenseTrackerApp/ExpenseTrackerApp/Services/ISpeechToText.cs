using System;

namespace ExpenseTrackerApp.Services
{
    public interface ISpeechToText
    {
        void Start();
        void Stop();
        event EventHandler<EventArgsVoiceRecognition> TextChanged;
    }
}


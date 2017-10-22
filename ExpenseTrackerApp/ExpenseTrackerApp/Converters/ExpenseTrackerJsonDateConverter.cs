using Newtonsoft.Json.Converters;

namespace ExpenseTrackerApp.Converters
{
    public class ExpenseTrackerJsonDateConverter : IsoDateTimeConverter
    {
        public ExpenseTrackerJsonDateConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}

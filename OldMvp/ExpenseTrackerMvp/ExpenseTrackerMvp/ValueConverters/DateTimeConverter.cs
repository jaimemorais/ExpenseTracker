using System;
using System.Globalization;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.ValueConverters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var datetime = (DateTime)value;
            
            return datetime.ToString("dd/MM/yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // todo : fix

            if (value == null)
                return string.Empty;

            var datetime = (DateTime)value;
            
            return datetime.ToString("dd/MM/yyyy");
        }
    }
}

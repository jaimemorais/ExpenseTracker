using System;
using Xamarin.Forms;

namespace ExpenseTrackerApp.CustomRenderers
{
    public class DecimalEntry : Entry
    {
        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(int), typeof(DecimalEntry), 0);

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public bool ShouldReactToTextChanges { get; set; }

        public DecimalEntry()
        {
            ShouldReactToTextChanges = true;
        }

        public static int DumbParse(string input)
        {
            if (input == null) return 0;

            var number = 0;
            int multiply = 1;

            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (Char.IsDigit(input[i]))
                {
                    number += (input[i] - '0') * (multiply);
                    multiply *= 10;
                }
            }
            return number;
        }

        public static string ConvertNumber(int value)
        {
            string number = String.Empty;

            number = value.ToString().Replace(",", "").Replace(".", "");

            if (number.Equals("")) number = "000";

            number = number.PadLeft(3, '0');

            if (number.Length > 3 && number.Substring(0, 1).Equals("0"))
            {
                number = number.Substring(1, number.Length - 1);
            }

            double finalValue = Convert.ToDouble(number) / 100;

            return finalValue.ToString();
        }

    }
}

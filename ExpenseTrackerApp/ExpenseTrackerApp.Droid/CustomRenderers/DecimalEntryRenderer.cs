using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpenseTrackerApp.CustomRenderers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Views;
using System;
using System.ComponentModel;
using ExpenseTrackerApp.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;



[assembly: ExportRenderer(typeof(DecimalEntry), typeof(DecimalEntryDroidRenderer))]
namespace ExpenseTrackerApp.Droid.CustomRenderers
{
    public class DecimalEntryDroidRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);


            if (e.OldElement != null)
            {
                Control.AfterTextChanged -= Control_AfterTextChanged;
            }

            if (e.NewElement != null)
            {
                Control.AfterTextChanged += Control_AfterTextChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(DecimalEntry.Value)))
            {
                var element = ((DecimalEntry)Element);

                // Format number, and place the formatted text in newText
                var formattedValue = DecimalEntry.ConvertNumber(element.Value);

                if (element.Value == 0) formattedValue = "";

                var newText = formattedValue;

                // Set the Text property of our control to newText
                Control.Text = newText;
            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }

        }



        void Control_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            var element = ((DecimalEntry)Element);
            
            // 1. Stop listening for changes on our control Text property
            if (!element.ShouldReactToTextChanges) return;
            element.ShouldReactToTextChanges = false;

            // 2. Get the current cursor position
            var cursorPosition = Control.SelectionStart;

            // 3. Take the control’s text, lets name it oldText
            var oldText = Control.Text;

            // 4. Parse oldText into a number, lets name it number
            int number = DecimalEntry.DumbParse(oldText);
            element.Value = number;

            // 5. Format number, and place the formatted text in newText
            var valorFormatado = DecimalEntry.ConvertNumber(number);

            if (number == 0) valorFormatado = "";

            var newText = valorFormatado;

            // 6. Set the Text property of our control to newText
            Control.Text = newText;

            // 7. Calculate the new cursor position
            var change = oldText.Length - newText.Length;

            // 8. Set the new cursor position
            int newPos = cursorPosition - change;
            Control.SetSelection(newPos < 0 ? 0 : newPos);

            // 9. Start listening for changes on our control’s Text property
            element.ShouldReactToTextChanges = true;
        }


    }
}
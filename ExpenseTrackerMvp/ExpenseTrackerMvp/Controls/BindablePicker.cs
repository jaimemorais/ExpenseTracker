namespace ExpenseTrackerMvp.Controls
{
    /* Not used anymore. Updated to Xamarin Forms 2.3.4 that now has a bindable picker 
     * public class BindablePicker : Picker
    {
        public BindablePicker()
        {
            this.SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public static BindableProperty ItemsSourceProperty =
                BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(BindablePicker), null,
                    propertyChanged: OnItemsSourceChanged);

        public static BindableProperty SelectedItemProperty =
                BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(BindablePicker), null,
                    propertyChanged: OnSelectedItemChanged);


        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }


        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = bindable as BindablePicker;
            picker.Items.Clear();
            if (newvalue != null)
            {
                //now it works like "subscribe once" but you can improve
                foreach (var item in (IEnumerable)newvalue)
                {
                    picker.Items.Add(item.ToString());
                }
            }

            ((INotifyCollectionChanged)picker.ItemsSource).CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        break;
                    case NotifyCollectionChangedAction.Add:
                        break;
                    case NotifyCollectionChangedAction.Move:
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        break;
                    case NotifyCollectionChangedAction.Replace:
                        break;
                }
            };
        }


        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = Items[SelectedIndex];
            }
        }


        private static void OnSelectedItemChanged(BindableObject bindable,
            object oldvalue, object newvalue)
        {
            var picker = bindable as BindablePicker;
            if (newvalue != null)
            {
                picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());
            }
        }
    }*/
}

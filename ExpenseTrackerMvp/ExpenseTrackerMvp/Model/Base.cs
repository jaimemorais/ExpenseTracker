using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExpenseTrackerMvp.Model
{
    public abstract class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

using Xamarin.Forms;

namespace ExpenseTrackerMvp.View
{
    public partial class ShowGifPage : ContentPage
    {
        public ShowGifPage(string imageToShow)
        {
            InitializeComponent();

            imgToShow.Source = imageToShow;
        }
    }
}

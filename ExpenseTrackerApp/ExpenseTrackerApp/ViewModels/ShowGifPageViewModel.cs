using Prism.Navigation;

namespace ExpenseTrackerApp.ViewModels
{
    public class ShowGifPageViewModel : BaseViewModel, INavigatingAware
    {

        private string _imageToShow;
        public string ImageToShow
        {
            get { return _imageToShow; }
            set { SetProperty(ref _imageToShow, value); }
        }


        public ShowGifPageViewModel()
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            ImageToShow = (string)parameters["imageToShow"];
        }
    }
}

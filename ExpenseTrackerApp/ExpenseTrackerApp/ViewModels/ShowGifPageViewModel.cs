using Prism.Navigation;

namespace ExpenseTrackerApp.ViewModels
{
    public class ShowGifPageViewModel : BaseViewModel, INavigatingAware, INavigatedAware
    {

        private string _imageToShow;
        public string ImageToShow
        {
            get { return _imageToShow; }
            set { SetProperty(ref _imageToShow, value); }
        }

        private readonly INavigationService _navigationService;

        public ShowGifPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            ImageToShow = (string)parameters["imageToShow"];
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}

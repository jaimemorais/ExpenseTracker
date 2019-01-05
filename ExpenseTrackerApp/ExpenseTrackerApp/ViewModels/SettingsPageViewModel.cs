using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Settings;
using Plugin.FirebasePushNotification;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace ExpenseTrackerApp.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {


        public DelegateCommand RefreshCachedListsCommand => new DelegateCommand(async () => await ExecuteRefreshCachedLists());
        public DelegateCommand UpdatePushTokenCommand => new DelegateCommand(async () => await ExecuteRefreshPushToken());



        private readonly IExpenseTrackerService _expenseTrackerService;
        private readonly INavigationService _navigationService;
        private readonly IUserSettings _userSettings;
        private readonly ITelemetry _telemetry;


        public SettingsPageViewModel(IExpenseTrackerService expenseTrackerService, 
            INavigationService navigationService,
            IUserSettings userSettings, ITelemetry telemetry)
        {
            _expenseTrackerService = expenseTrackerService;
            _navigationService = navigationService;
            _userSettings = userSettings;
            _telemetry = telemetry;

        }

        

        public async Task ExecuteRefreshCachedLists()
        {            
            IsBusy = true;

            try
            {
                _userSettings.SetCategoriesLocal(await _expenseTrackerService.GetCategoryListAsync());                
                _userSettings.SetPaymentTypesLocal(await _expenseTrackerService.GetPaymentTypeListAsync());

                await base.ShowMessageAsync("Cached Lists Updated!");
            }
            catch (Exception ex)
            {
                _telemetry.LogError("ExecuteRefreshCachedLists error", ex);
                await base.ShowErrorMessageAsync("Error.");
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private async Task ExecuteRefreshPushToken()
        {
            try
            {
                await _expenseTrackerService.UpdateUserFCMToken(CrossFirebasePushNotification.Current.Token);

                await base.ShowMessageAsync("Push Token Updated!");
            }
            catch (Exception ex)
            {
                _telemetry.LogError("ExecuteRefreshCachedLists error", ex);
                await base.ShowErrorMessageAsync("Error.");
            }
            finally
            {
                IsBusy = false;
            }
            

                
        }

    }
}

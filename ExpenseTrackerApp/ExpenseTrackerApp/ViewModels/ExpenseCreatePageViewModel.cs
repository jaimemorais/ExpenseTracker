using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Settings;
using ExpenseTrackerApp.Views;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackerApp.ViewModels
{
    public class ExpenseCreatePageViewModel : BaseViewModel, INavigatedAware
    {

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { SetProperty(ref _date, value); }
        }

        private string _description;
        public string Description 
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
                
        public double Value { get; set; }
        public string Category { get; set; }
        public string PaymentType { get; set; }


        public ObservableCollection<string> CategoryList { get; }

        private string _categorySelectedItem;
        public string CategorySelectedItem
        {
            get { return _categorySelectedItem; }
            set { SetProperty(ref _categorySelectedItem, value); }            
        }

        public ObservableCollection<string> PaymentTypeList { get; }

        private string _paymentTypeSelectedItem;
        public string PaymentTypeSelectedItem
        {
            get { return _paymentTypeSelectedItem; }
            set { SetProperty(ref _paymentTypeSelectedItem, value); }
        }




        public DelegateCommand SaveCommand => new DelegateCommand(async () => await ExecuteSaveAsync());
        
        public DelegateCommand BackCommand => new DelegateCommand(async () => await ExecuteBackAsync());


        
        private readonly IExpenseTrackerService _expenseTrackerService;
        private readonly INavigationService _navigationService;
        private readonly IUserSettings _userSettings;
        private readonly ITelemetry _telemetry;


        public ExpenseCreatePageViewModel(IExpenseTrackerService expenseTrackerService, 
            INavigationService navigationService,
            IUserSettings userSettings, ITelemetry telemetry)
        {
            _expenseTrackerService = expenseTrackerService;
            _navigationService = navigationService;
            _userSettings = userSettings;
            _telemetry = telemetry;

            PaymentTypeList = new ObservableCollection<string>();

            CategoryList = new ObservableCollection<string>();
        }




        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            this.Date = DateTime.Now.Date;
            this.Description = string.Empty;            

            await ExecuteLoadCategoriesAsync();
            await ExecuteLoadPaymentTypesAsync();

        }


        public async Task ExecuteLoadCategoriesAsync()
        {

            try
            {
                IsBusy = true;

                CategoryList.Clear();

                List<Category> listCat = await _expenseTrackerService.GetCategoryListAsync();

                foreach (Category cat in listCat)
                {
                    CategoryList.Add(cat.Name);
                }

            }
            catch (Exception ex)
            {
                await base.ShowErrorMessageAsync("Error getting category list : " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }

        public async Task ExecuteLoadPaymentTypesAsync()
        {

            try
            {
                IsBusy = true;

                PaymentTypeList.Clear();

                List<PaymentType> listPaymentType = await _expenseTrackerService.GetPaymentTypeListAsync();

                foreach (PaymentType pt in listPaymentType)
                {
                    PaymentTypeList.Add(pt.Name);
                }

            }
            catch (Exception ex)
            {
                await base.ShowErrorMessageAsync("Error getting payment type list : " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }




        int _tapCount = 0;

        private async Task ExecuteSaveAsync()
        {
            // Prevent multiple tap on save button
            _tapCount += 1;
            if (_tapCount > 1)
            {
                _tapCount = 0;
                return;
            }
            

            IsBusy = true;

            try
            {
                Expense exp = new Expense();
                exp.Date = this.Date;

                if (this.CategorySelectedItem == null)
                {
                    await base.ShowErrorMessageAsync("Select a category.");
                    return;
                }
                exp.Category = this.CategorySelectedItem;

                if (this.Value == 0)
                {
                    await base.ShowErrorMessageAsync("Inform a value.");
                    return;
                }
                exp.Value = this.Value;

                if (this.PaymentTypeSelectedItem == null)
                {
                    await base.ShowErrorMessageAsync("Select a payment type.");
                    return;
                }
                exp.PaymentType = this.PaymentTypeSelectedItem;


                if (string.IsNullOrEmpty(this.Description))
                {
                    exp.Description = exp.Category;
                }
                else
                {
                    exp.Description = this.Description;
                }


                exp.UserName = _userSettings.GetEmail();


                if (await _expenseTrackerService.SaveExpenseAsync(exp))
                {
                    await NavigateToExpenseList(exp);
                }
                else
                {
                    await base.ShowErrorMessageAsync("Error creating Expense on server.");
                }
            }
            catch (Exception ex)
            {
                _telemetry.LogError("ExecuteLoadExpensesAsync error", ex);
                await base.ShowErrorMessageAsync("Error creating Expense on server.");
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private string GetImageToShow(Expense exp)
        {
            string img_s = "puppys{0}.png";
            string img_h = "puppyh{0}.png";

            Random rnd = new Random();


            if ((DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday) && exp.Value > 30) || exp.Value > 80)
            {
                int r = rnd.Next(1, 7);
                return string.Format(img_s, r);
            }
            else if ((DateTime.Now.DayOfWeek.Equals(DayOfWeek.Tuesday) && exp.Value > 40))
            {
                int r = rnd.Next(1, 7);
                return string.Format(img_s, r);
            }
            else
            {
                int r = rnd.Next(1, 9);
                return string.Format(img_h, r);
            }

        }

        private async Task NavigateToExpenseList(Expense exp)
        {
            
            if (_userSettings.GetShowPuppyPref())
            {
                // Show image before navigating back to list
                string imageToShow = GetImageToShow(exp);
                NavigationParameters navParameters = new NavigationParameters();
                navParameters.Add("imageToShow", imageToShow);
                await _navigationService.NavigateAsync(nameof(ShowGifPage), navParameters, null, true);
                await Task.Delay(1500);
            }
            
            await _navigationService.NavigateAsync($"ExpenseTrackerApp:///{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(ExpenseListPage)}");
        }
        

        private async Task ExecuteBackAsync()
        {
            await _navigationService.GoBackAsync();
        }
    }
}

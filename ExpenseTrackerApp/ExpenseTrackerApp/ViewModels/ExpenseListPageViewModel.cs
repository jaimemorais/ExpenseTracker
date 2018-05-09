using ExpenseTrackerApp.Helpers;
using ExpenseTrackerApp.Model;
using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Settings;
using ExpenseTrackerApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTrackerApp.ViewModels
{
    public class ExpenseListPageViewModel : BaseViewModel, INavigatedAware
    {

        public ObservableCollection<Expense> ExpenseList { get; set; }

        public DelegateCommand LoadExpensesCommand => new DelegateCommand(async () => await ExecuteLoadExpensesAsync());

        public DelegateCommand AddExpenseCommand => new DelegateCommand(async () => await AddExpenseAsync());
        
        public DelegateCommand<Expense> DeleteExpenseCommand => new DelegateCommand<Expense>(async (exp) => await DeleteExpenseAsync(exp));

        public DelegateCommand LogoffCommand => new DelegateCommand(async () => await ExecuteLogoffAsync());


        public DelegateCommand<string> AddExpenseVoiceCommand => new DelegateCommand<string>(async (voiceText) => await AddExpenseVoiceAsync(voiceText));


        private List<Category> categories;
        private List<PaymentType> paymentTypes;



        private readonly IExpenseTrackerService _expenseTrackerService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IFirebaseService _firebaseService;
        private readonly ITelemetry _telemetry;
        private readonly IUserSettings _userSettings;

        public ExpenseListPageViewModel(IExpenseTrackerService expenseTrackerService, INavigationService navigationService, IPageDialogService pageDialogService,
            IFirebaseService firebaseService, IUserSettings userSettings, ITelemetry telemetry)
        {
            _expenseTrackerService = expenseTrackerService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
            _firebaseService = firebaseService;
            _userSettings = userSettings;

            _telemetry = telemetry;

            ExpenseList = new ObservableCollection<Expense>();

        }




        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }


        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            categories = _userSettings.GetCategoriesLocal();
            if (categories == null)
            {
                categories = await _expenseTrackerService.GetCategoryListAsync();
                _userSettings.SetCategoriesLocal(categories);
            }

            paymentTypes = _userSettings.GetPaymentTypesLocal();
            if (paymentTypes == null)
            {
                paymentTypes = await _expenseTrackerService.GetPaymentTypeListAsync();
                _userSettings.SetPaymentTypesLocal(paymentTypes);
            }

            await ExecuteLoadExpensesAsync();
            Xamarin.Forms.DependencyService.Get<IKeyboardHelper>().HideKeyboard();

        }

        private async Task ExecuteLoadExpensesAsync()
        {
            IsBusy = true;

            try
            {
                ExpenseList.Clear();

                IList<Expense> list = await _expenseTrackerService.GetExpenseListAsync();

                foreach (Expense e in list)
                {
                    ExpenseList.Add(e);
                }
            }
            catch (Exception ex)
            {
                _telemetry.LogError("ExecuteLoadExpensesAsync error", ex);
            }
            finally
            {
                IsBusy = false;
            }

        }


        private async Task AddExpenseAsync()
        {
            await _navigationService.NavigateAsync(nameof(ExpenseCreatePage));
        }

        private async Task DeleteExpenseAsync(Expense exp)
        {
            if (await _expenseTrackerService.DeleteExpenseAsync(exp))
            {
                await ExecuteLoadExpensesAsync();
            }
            else
            {
                await base.ShowErrorMessageAsync("Error deleting Expense on server.");
            }
        }

        

        private async Task ExecuteLogoffAsync()
        {
            _firebaseService.Logout();

            await _navigationService.NavigateAsync($"ExpenseTrackerApp:///{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }


        private async Task AddExpenseVoiceAsync(string text)
        {            
            IsBusy = true;

            try
            {
                Expense exp = CreateExpenseFromText(text);                

                if (exp != null)
                {
                    if (await _expenseTrackerService.SaveExpenseAsync(exp))
                    {
                        await ExecuteLoadExpensesAsync();
                    }
                    else
                    {
                        await base.ShowErrorMessageAsync("Error creating Expense on server.");
                    }
                }
                else
                {
                    await base.ShowErrorMessageAsync("Error creating Expense from text spoken.");
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



        private Expense CreateExpenseFromText(string textSpoken)
        {
            try
            {
                string[] wordsSpoken = textSpoken.Split(' ');
                string categorySpoken = RemoveAccents(wordsSpoken[0]);
                string valueSpoken = wordsSpoken[1];
                string paymentTypeSpoken = RemoveAccents(wordsSpoken[2]);
                
                string descriptionSpoken = string.Empty;
                if (wordsSpoken.Length > 3)
                {
                    int i = 3;
                    do
                    {
                        descriptionSpoken += RemoveAccents(wordsSpoken[i]) + " ";
                        i++;
                    } while (i < wordsSpoken.Length);
                }


                Expense exp = new Expense();

                exp.Date = DateTime.Today;

                exp.Value = decimal.Parse(valueSpoken);



                string originalCategorySpoken = categorySpoken;
                if (RemoveAccents(categorySpoken).ToLower() == "lanche" || RemoveAccents(categorySpoken).ToLower() == "almoco")
                {
                    categorySpoken = "AlimRua";
                }
                exp.Category = categories.FirstOrDefault(c => c.Name.ToLower().Equals(categorySpoken.ToLower())).Name; 

                


                if (RemoveAccents(paymentTypeSpoken).ToLower() == "alimentacao")
                {
                    paymentTypeSpoken = "Ticket Alim";
                }
                else if (RemoveAccents(paymentTypeSpoken).ToLower() == "refeicao")
                {
                    paymentTypeSpoken = "Ticket Rest";
                }
                else if (RemoveAccents(paymentTypeSpoken).ToLower() == "cartao")
                {
                    paymentTypeSpoken = "Cartao Credito";
                }
                exp.PaymentType = paymentTypes.FirstOrDefault(p => p.Name.ToLower().Equals(paymentTypeSpoken.ToLower())).Name;



                if (string.IsNullOrEmpty(descriptionSpoken))
                    descriptionSpoken = originalCategorySpoken;
                exp.Description = descriptionSpoken;
                
                exp.UserName = _userSettings.GetEmail();

                return exp;
            }
            catch
            {
                return null;
            }
        }

        

        private string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

    }
}

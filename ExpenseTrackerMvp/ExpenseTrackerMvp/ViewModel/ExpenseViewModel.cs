using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Service;
using ExpenseTrackerMvp.Util;
using ExpenseTrackerMvp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.ViewModel
{
    public class ExpenseViewModel : BaseViewModel
    {
        public ObservableCollection<Model.Expense> ExpenseCollection { get; }
        
        private readonly IExpenseTrackerWebApiClientService _expenseTrackerWebApiService;

        public ExpenseViewModel(IExpenseTrackerWebApiClientService expenseTrackerWebApiService)
        {
            _expenseTrackerWebApiService = expenseTrackerWebApiService;

            ExpenseCollection = new ObservableCollection<Expense>();            
        }
        

        public Command LoadExpensesCommand => new Command(ExecuteLoadExpense);

        public Command CreateCommand => new Command(ExecuteCreate);

        public Command DeleteItemCommand => new Command<Expense>(ExecuteDeleteItem);

        public Command LogoffCommand => new Command(ExecuteLogoff);




        private async void ExecuteLogoff()
        {
            await UserSettings.Instance.SaveEmailAsync("");
            await UserSettings.Instance.SavePasswordAsync("");
            App.Current.MainPage = new LoginPage();
        }


        private async void ExecuteDeleteItem(Expense exp)
        {
            HttpResponseMessage httpResponse = await _expenseTrackerWebApiService.DeleteExpenseAsync(exp);

            if (httpResponse.IsSuccessStatusCode)
            {
                ExecuteLoadExpense();
            }
            else
            {
                await base.ShowErrorMessage("Error deleting Expense on server.");
            }            
        }


        private async void ExecuteCreate()
        {
            await App.NavigateMasterDetailModal(new ExpenseCreatePage());
        }

        

        private async void ExecuteLoadExpense()
        {

            ExpenseCollection.Clear();

            try
            {
                IsBusy = true;

                
                List<Expense> expenseList = await _expenseTrackerWebApiService.GetExpenseListAsync();
                
                foreach (Expense exp in expenseList)
                {
                    this.ExpenseCollection.Add(exp);
                }
                                
            }
            catch (Exception ex)
            {
                
                await base.ShowErrorMessage("Cannot connect to server. Try again.");
                
                // TODO logging
                string log = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }


            // Using Firebase
            // https://github.com/rlamasb/Firebase.Xamarin
            // https://github.com/williamsrz/xamarin-on-fire/blob/master/XOF.Droid/Services/FirebaseService.cs
            /*
            string firebaseToken = UserSettings.GetFirebaseAuthToken();
            var firebase = FirebaseService.GetFirebaseExpenseTrackerClient();

            // Example : get one
            //Expense exp = await firebase.Child("Expenses").Child("1").WithAuth(firebaseToken).OnceSingleAsync<Expense>();
            //ExpenseCollection.Add(exp);
                        
            var items = await firebase
              .Child("Expenses")
              .OrderByKey()
              .WithAuth(firebaseToken)                                         
              .OnceAsync<IList<Expense>>();
                        
            foreach (var item in items)
            {
                Expense exp = (Expense)item.Object;
                ExpenseCollection.Add(exp);                
            }
            */

        }

    }
}
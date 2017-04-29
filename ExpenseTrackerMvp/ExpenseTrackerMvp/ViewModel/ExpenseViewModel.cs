using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Service;
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
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public Double Value { get; set; }
        public String Category { get; set; }
        public String PaymentType { get; set; }

        public ObservableCollection<Model.Expense> ExpenseCollection
        {
            get;
            set;
        }


        public List<Category> CategoryList { get; set; }
        public Category CategorySelectedItem { get; set; }

        private bool _busy;        
        public bool IsBusy
        {
            get
            {
                return _busy;
            }
            set
            {
                _busy = value;
                this.Notify();
            }
        }

        


        private readonly IExpenseTrackerWebApiClientService _expenseTrackerWebApiService;

        public ExpenseViewModel(IExpenseTrackerWebApiClientService expenseTrackerWebApiService)
        {
            _expenseTrackerWebApiService = expenseTrackerWebApiService;

            ExpenseCollection = new ObservableCollection<Expense>();

            LoadCommand = new Command(ExecuteLoadExpense);

            CreateCommand = new Command(ExecuteCreate);

            SaveCommand = new Command(ExecuteSave);

            BackCommand = new Command(ExecuteBack);

        }




        public Command LoadCommand
        {
            get;
            set;
        }

        public Command CreateCommand
        {
            get;
            set;
        }

        public Command SaveCommand
        {
            get;
            set;
        }

        public Command BackCommand
        {
            get;
            set;
        }




        private async void ExecuteCreate()
        {
            await ExecuteLoadCategory();

            await App.NavigateMasterDetailModal(new ExpensesCreatePage());
        }


        private async void ExecuteBack()
        {
            await App.NavigateMasterDetailModalBack();
        }

        private async void ExecuteSave()
        {
            Expense exp = new Expense();
            exp.Date = this.Date;
            exp.Description = this.Description;
            exp.Value = this.Value;

            /*
            if (this.CategorySelectedItem == null)
            {
                await base.ShowErrorMessage("Select a category.");
                return;
            }
            exp.Category = this.CategorySelectedItem.Name;
            */


            HttpResponseMessage httpResponse = await _expenseTrackerWebApiService.SaveExpense(exp);

            if (httpResponse.IsSuccessStatusCode)
            {
                LoadCommand.Execute(null);

                await App.NavigateMasterDetailModalBack();
            }
            else
            {
                await base.ShowErrorMessage("Error creating Expense on server.");
            }
        }


        private async void ExecuteLoadExpense()
        {

            ExpenseCollection.Clear();

            try
            {
                IsBusy = true;

                
                List<Expense> expenseList = await _expenseTrackerWebApiService.GetExpenseList();
                
                foreach (Expense exp in expenseList)
                {
                    this.ExpenseCollection.Add(exp);
                }
                                
            }
            catch (Exception ex)
            {
                // TODO logging
                await base.ShowErrorMessage("Cannot connect to server. " + ex.Message);
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
        

        private async System.Threading.Tasks.Task ExecuteLoadCategory()
        {

            try
            {
                if (CategoryList == null)
                {
                    CategoryList = new List<Model.Category>();
                }

                CategoryList.Clear();

                IsBusy = true;

                CategoryList = await _expenseTrackerWebApiService.GetCategoryList();

            }
            catch (Exception ex)
            {
                await base.ShowErrorMessage("Cannot connect to server. " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }


    }
}
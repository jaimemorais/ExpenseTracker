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


        public List<Category> CategoryList { get; set; }
        public Category CategorySelectedItem { get; set; }




        private bool Busy;        

        public bool IsBusy
        {
            get
            {
                return Busy;
            }
            set
            {
                Busy = value;
                this.Notify();
            }
        }

        public ObservableCollection<Model.Expense> ExpenseCollection
        {
            get;
            set;
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

        private readonly IExpenseTrackerWebApiService _expenseTrackerWebApiService;

        public ExpenseViewModel(IExpenseTrackerWebApiService expenseTrackerWebApiService)
        {
            _expenseTrackerWebApiService = expenseTrackerWebApiService;

            ExpenseCollection = new ObservableCollection<Expense>();

            LoadCommand = new Command(ExecuteLoadExpenses);            

            CreateCommand = new Command(ExecuteCreate);

            SaveCommand = new Command(ExecuteSave);

            BackCommand = new Command(ExecuteBack);

        }

        private async void ExecuteCreate()
        {
            await LoadCategoryList();

            await App.NavigateMasterDetailModal(new ExpensesCreatePage());
        }


        private async void ExecuteBack()
        {
            await App.NavigateMasterDetailModalBack();
        }

        private async System.Threading.Tasks.Task LoadCategoryList()
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


        public async void ExecuteLoadExpenses()
        {

            ExpenseCollection.Clear();

            try
            {
                IsBusy = true;

                this.ExpenseCollection = await _expenseTrackerWebApiService.GetExpenseList();
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

    }
}
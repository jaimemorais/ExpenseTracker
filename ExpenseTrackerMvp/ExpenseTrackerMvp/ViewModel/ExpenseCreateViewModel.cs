using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.ViewModel
{
    public class ExpenseCreateViewModel : BaseViewModel
    {
        public DateTime Date { get; set; }
        public String Description { get; set; }
        public Double Value { get; set; }
        public String Category { get; set; }
        public String PaymentType { get; set; }

        public ObservableCollection<string> CategoryList { get; set; }



        private string _categorySelectedItem;
        public string CategorySelectedItem
        {
            get { return _categorySelectedItem; }
            set
            {
                _categorySelectedItem = value;
                Notify();
            }
        }




        private readonly IExpenseTrackerWebApiClientService _expenseTrackerWebApiService;

        public ExpenseCreateViewModel(IExpenseTrackerWebApiClientService expenseTrackerWebApiService)
        {
            _expenseTrackerWebApiService = expenseTrackerWebApiService;

            SaveCommand = new Command(ExecuteSave);

            BackCommand = new Command(ExecuteBack);

            LoadCategoriesCommand = new Command(ExecuteLoadCategories);

            CategoryList = new ObservableCollection<string>();

            CategoryList.Add("Teste1");
            CategoryList.Add("Teste2");

        }

        
        public Command SaveCommand { get; set; }

        public Command BackCommand { get; set; }

        public Command LoadCategoriesCommand { get; set; }
        


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
                await App.NavigateMasterDetailModalBack();
            }
            else
            {
                await base.ShowErrorMessage("Error creating Expense on server.");
            }
        }

        

        public async void ExecuteLoadCategories()
        {

            try
            {
                IsBusy = true;

                CategoryList.Clear();

                List<Category> listCat = await _expenseTrackerWebApiService.GetCategoryList();

                foreach (Category cat in listCat)
                {
                    CategoryList.Add(cat.Name);
                }

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
using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Service;
using System;
using System.Collections.Generic;
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

        public List<Category> CategoryList { get; set; }
        public Category CategorySelectedItem { get; set; }

        


        private readonly IExpenseTrackerWebApiClientService _expenseTrackerWebApiService;

        public ExpenseCreateViewModel(IExpenseTrackerWebApiClientService expenseTrackerWebApiService)
        {
            _expenseTrackerWebApiService = expenseTrackerWebApiService;

            SaveCommand = new Command(ExecuteSave);

            BackCommand = new Command(ExecuteBack);

            LoadCategoryCommand = new Command(ExecuteLoadCategory);
        }

        
        public Command SaveCommand { get; set; }

        public Command BackCommand { get; set; }

        public Command LoadCategoryCommand { get; set; }


        
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

        

        private async void ExecuteLoadCategory()
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
using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Service;
using ExpenseTrackerMvp.Util;
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



        public ObservableCollection<string> CategoryList { get; }
        
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

        public ObservableCollection<string> PaymentTypeList { get; }

        private string _paymentTypeSelectedItem;
        public string PaymentTypeSelectedItem
        {
            get { return _paymentTypeSelectedItem; }
            set
            {
                _paymentTypeSelectedItem = value;
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

            LoadPaymentTypesCommand = new Command(ExecuteLoadPaymentTypes);

            CategoryList = new ObservableCollection<string>();

            PaymentTypeList = new ObservableCollection<string>();
        }

        
        public Command SaveCommand { get; set; }

        public Command BackCommand { get; set; }

        public Command LoadCategoriesCommand { get; set; }

        public Command LoadPaymentTypesCommand { get; set; }



        private async void ExecuteBack()
        {
            await App.NavigateMasterDetailModalBack(null);
        }

        private async void ExecuteSave()
        {
            Expense exp = new Expense();
            exp.Date = this.Date;



            if (this.CategorySelectedItem == null)
            {
                await base.ShowErrorMessage("Select a category.");
                return;
            }
            exp.Category = this.CategorySelectedItem;

            if (this.Value == 0)
            {
                await base.ShowErrorMessage("Inform a value.");
                return;
            }
            exp.Value = this.Value;

            if (this.PaymentTypeSelectedItem == null)
            {
                await base.ShowErrorMessage("Select a payment type.");
                return;
            }
            exp.PaymentType = this.PaymentTypeSelectedItem;

            if (this.Description == null)
            {
                await base.ShowErrorMessage("Inform a description.");
                return;
            }
            exp.Description = this.Description;


            exp.UserName = UserSettings.GetEmail();


            HttpResponseMessage httpResponse = await _expenseTrackerWebApiService.SaveExpense(exp);

            if (httpResponse.IsSuccessStatusCode)
            {


                await App.NavigateMasterDetailModalBack("teste");
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

        public async void ExecuteLoadPaymentTypes()
        {

            try
            {
                IsBusy = true;

                PaymentTypeList.Clear();

                List<PaymentType> listPaymentType = await _expenseTrackerWebApiService.GetPaymentTypeList();

                foreach (PaymentType pt in listPaymentType)
                {
                    PaymentTypeList.Add(pt.Name);
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
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

            PaymentTypeList = new ObservableCollection<string>();

            CategoryList = new ObservableCollection<string>();
        }

        
        public Command SaveCommand => new Command(ExecuteSave);

        public Command BackCommand => new Command(ExecuteBack);

        public Command LoadCategoriesCommand => new Command(ExecuteLoadCategories);

        public Command LoadPaymentTypesCommand => new Command(ExecuteLoadPaymentTypes);



        private async void ExecuteBack()
        {
            await App.NavigateMasterDetailModalBack(null);
        }


        int _tapCount = 0;

        private async void ExecuteSave()
        {
            // Prevent multiple tap on save button
            _tapCount += 1;
            if (_tapCount > 1)
            {
                _tapCount = 0;
                return;
            }

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

                string imageToShow = GetImageToShow(exp);
                await App.NavigateMasterDetailModalBack(imageToShow);
            }
            else
            {
                await base.ShowErrorMessage("Error creating Expense on server.");
            }
        }

        private string GetImageToShow(Expense exp)
        {
            string img_s = "puppys{0}.png";
            string img_h = "puppyh{0}.png";

            Random rnd = new Random();
            int r = rnd.Next(1, 7);

            if ((DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday) && exp.Value > 30) || exp.Value > 80) 
            {
                return string.Format(img_s, r);
            }

            if ((DateTime.Now.DayOfWeek.Equals(DayOfWeek.Tuesday) && exp.Value > 40))
            {
                return string.Format(img_s, r);
            }
            
            return string.Format(img_h, r);
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
using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
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

        public ExpenseViewModel()
        {
            this.ExpenseCollection = new ObservableCollection<Model.Expense>();

            LoadCommand = new Command(ExecuteLoad);

            LoadCommand.Execute(null);

            CreateCommand = new Command(ExecuteCreate);

            SaveCommand = new Command(ExecuteSave);
        }

        private async void ExecuteCreate()
        {
            await LoadCategoryList();

            await App.NavigateMasterDetail(new ExpensesCreatePage());
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
                using (HttpClient httpClient = base.GetHttpClient())
                {
                    var response = await httpClient.GetAsync(GetApiServiceURL("Categories"));

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content;
                        var responseContent = await content.ReadAsStringAsync();

                        JArray json = JArray.Parse(responseContent);

                        foreach (JToken item in json)
                        {
                            JObject cat = (JObject)JsonConvert.DeserializeObject(item.ToString());

                            Category catItem = new Model.Category();
                            catItem.Name = cat["Name"].ToString();

                            CategoryList.Add(catItem);
                        }
                    }
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


        private async void ExecuteSave()
        {
            Expense exp = new Expense();
            exp.Date = this.Date;
            exp.Description = this.Description;
            exp.Value = this.Value;

            if (this.CategorySelectedItem == null)
            {
                await base.ShowErrorMessage("Select a category.");
                return;
            }
            exp.Category = this.CategorySelectedItem.Name;

            string json = JsonConvert.SerializeObject(exp);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await base.GetHttpClient().PostAsync(GetApiServiceURL("Expenses"), httpContent);
            
            if (httpResponse.IsSuccessStatusCode)
            {
                LoadCommand.Execute(null);
                await App.NavigateMasterDetail(new ExpensesPage());
            }
            else
            {
                await base.ShowErrorMessage("Error creating Expense on server.");
            }
        }


        private async void ExecuteLoad(object obj)
        {

            ExpenseCollection.Clear();

            try
            {
                IsBusy = true;
                using (HttpClient httpClient = base.GetHttpClient())
                {
                    var response = await httpClient.GetAsync(GetApiServiceURL("Expenses"));

                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content;
                        var responseContent = await content.ReadAsStringAsync();

                        JArray json = JArray.Parse(responseContent);

                        foreach (JToken item in json)
                        {
                            Expense exp = new Expense();
                            JObject e = (JObject)JsonConvert.DeserializeObject(item.ToString());
                            exp.Description = e["Description"].ToString();
                            exp.Value = double.Parse(e["Value"].ToString());
                            exp.Date = DateTime.Parse(e["Date"].ToString());

                            ExpenseCollection.Add(exp);
                        }
                    }
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

    }
}
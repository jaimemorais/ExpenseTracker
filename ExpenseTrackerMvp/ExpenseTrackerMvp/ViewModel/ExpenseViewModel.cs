using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.ViewModel
{
    public class ExpenseViewModel : BaseViewModel
    {

        public ObservableCollection<Model.Expense> ExpenseCollection
        {
            get;
            set;
        }

        public ICommand LoadCommand
        {
            get;
            set;
        }

        public ICommand CreateCommand
        {
            get;
            set;
        }

        public ExpenseViewModel()
        {
            this.ExpenseCollection = new ObservableCollection<Model.Expense>();

            LoadCommand = new Command(Load);

            LoadCommand.Execute(null);

            CreateCommand = new Command(Create);
        }

        private async void Create()
        {
            await App.NavigateMasterDetail(new ExpensesCreatePage());
        }

        
        private async void Load(object obj)
        {
            // https://github.com/rlamasb/Firebase.Xamarin
            // https://github.com/williamsrz/xamarin-on-fire/blob/master/XOF.Droid/Services/FirebaseService.cs
            

            ExpenseCollection.Clear();


            // Using Firebase
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


            // Using custom api 
            string url = GetApiServiceURL("Expenses");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            httpClient.Timeout = new System.TimeSpan(0, 0, 3);

            var response = await httpClient.GetAsync(url);            

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
                    
                    ExpenseCollection.Add(exp);                    
                }
            }
            

        }

    }
}
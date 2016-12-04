using ExpenseTrackerMvp.Model;
using ExpenseTrackerMvp.Util;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Collections.ObjectModel;
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

        public ExpenseViewModel()
        {
            this.ExpenseCollection = new ObservableCollection<Model.Expense>();

            LoadCommand = new Command(Load);
        }

        
        private async void Load(object obj)
        {

            string firebaseApiKey = AppConfig.Instance.GetFirebaseApiKey();

            string user = ""; // get on firebase
            string pass = ""; // get on firebase

   

            // Firebase - Email/Password Auth
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseApiKey));
            var authLink = await authProvider.SignInWithEmailAndPasswordAsync(user, pass);
            string firebaseToken = authLink.FirebaseToken;
                        
            // https://github.com/rlamasb/Firebase.Xamarin
            // https://github.com/williamsrz/xamarin-on-fire/blob/master/XOF.Droid/Services/FirebaseService.cs

            var firebase = new FirebaseClient("https://expensetrackermvp.firebaseio.com/");

            // Get one
            Expense exp = await firebase.Child("Expenses").Child("1").WithAuth(firebaseToken).OnceSingleAsync<Expense>();
            ExpenseCollection.Add(exp);
            
            /*var items = await firebase
              .Child("Expenses")
              .OrderByKey()
              .WithAuth(firebaseToken)                           
              .OnceAsync<Expense>();
                        
            foreach (var item in items)
            {
                Expense exp = item.Object;
                ExpenseCollection.Add(exp);                
            }*/





            /* Using custom api 
            string url = GetApiServiceURL("Expenses");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            var response = httpClient.GetAsync(url).Result;            

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                var responseContent = content.ReadAsStringAsync().Result;

                JArray json = JArray.Parse(responseContent);
                                
                foreach (JToken item in json)
                {

                    //string desc = JToken.Parse(JToken.Parse(responseContent)[0])["Description"].Value;
                    //Expense exp = new Expense();
                    //exp.Description = desc.Value<string>();
                            
                    Expense e = item.ToObject<Expense>();                   
                    
                    ExpenseCollection.Add(e);                    
                }
            }
            */

        }

    }
}
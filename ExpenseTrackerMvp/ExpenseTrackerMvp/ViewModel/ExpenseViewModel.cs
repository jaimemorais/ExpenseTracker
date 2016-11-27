using ExpenseTrackerMvp.Model;
using Firebase.Xamarin.Database;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
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

        public ICommand CarregarCommand
        {
            get;
            set;
        }

        public ExpenseViewModel()
        {
            this.ExpenseCollection = new ObservableCollection<Model.Expense>();

            CarregarCommand = new Command(Carregar);
        }

        
        private void Carregar(object obj)
        {
            /* Using my api 
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


            // Using firebase

            var firebase = new FirebaseClient("https://expensetrackermvp.firebaseio.com/");

            //ChildQuery query = firebase.Child("Expenses");


            // https://github.com/rlamasb/Firebase.Xamarin
            // https://github.com/williamsrz/xamarin-on-fire/blob/master/XOF.Droid/Services/FirebaseService.cs
            
            var items = firebase
              .Child("Expenses")
              //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.              
              //.LimitToFirst(2)
              .OnceAsync<Expense>();


            //ExpenseCollection = new ObservableCollection<Expense>(items.Select(c => c.Object).ToList());

            /*foreach (FirebaseObject<Expense> item in items.Result)
            {
                //ExpenseCollection.Add(item.);
            }*/

        }
        
    }
}
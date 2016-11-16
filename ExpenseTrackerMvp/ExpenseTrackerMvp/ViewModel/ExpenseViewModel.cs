using ExpenseTrackerMvp.Model;
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
            /*
            Expenses.Add(new Model.Expense()
            {
                Data = DateTime.Today,
                Valor = 10.50,
                Categoria = "Compras",
                Descricao = "Loja " + Expenses.Count,
                TipoPagamento = "Ticket Alim"
            });*/

            
            string url = GetApiServiceURL("Expenses");
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            var response = httpClient.GetAsync(url).Result;
            
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                var responseContent = content.ReadAsStringAsync().Result;

                JArray json = JArray.Parse(responseContent);
                                
                foreach (JToken item in json)
                {
                    Expense e = item.ToObject<Expense>();

                    ExpenseCollection.Add(e);                    
                }
            }
        }
    }
}
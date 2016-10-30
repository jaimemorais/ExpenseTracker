using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTrackerMvp.ViewModel
{
    public class ExpenseViewModel
    {

        public ObservableCollection<Model.Expense> Expenses
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
            this.Expenses = new ObservableCollection<Model.Expense>();

            CarregarCommand = new Command(Carregar);
        }

        
        private void Carregar(object obj)
        {
            Expenses.Add(new Model.Expense()
            {
                Data = DateTime.Today,
                Valor = 10.50,
                Categoria = "Compras",
                Descricao = "Loja " + Expenses.Count,
                TipoPagamento = "Ticket Alim"
            });
            
        }
    }
}
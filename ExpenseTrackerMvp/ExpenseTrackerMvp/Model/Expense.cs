using System;

namespace ExpenseTrackerMvp.Model
{
    public class Expense : Base
    {
        private DateTime data;
        public DateTime Data 
        {
            get { return data; }
            set
            {
                data = value;
                this.Notify();
            }
        }

        private double valor;
        public double Valor
        {
            get { return valor; }
            set
            {
                valor = value;
                this.Notify();
            }
        }


        private string categoria;
        public string Categoria
        {
            get { return categoria; }
            set
            {
                categoria = value;
                this.Notify();
            }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                descricao = value;
                this.Notify();
            }
        }

        private string tipoPagamento;
        public string TipoPagamento
        {
            get { return tipoPagamento; }
            set
            {
                tipoPagamento = value;
                this.Notify();
            }
        }

    }
}
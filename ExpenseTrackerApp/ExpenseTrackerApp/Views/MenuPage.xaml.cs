﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackerApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }
    }
}
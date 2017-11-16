using DryIoc;
using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Settings;
using ExpenseTrackerApp.Views;
using Prism.DryIoc;
using Xamarin.Forms;

namespace ExpenseTrackerApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            Container.Register<ITelemetry, Telemetry>(Reuse.Singleton);
            Container.Register<IUserSettings, UserSettings>(Reuse.Singleton);
            Container.Register<IHttpConnection, HttpConnection>(Reuse.Singleton);
            Container.Register<IExpenseTrackerService, ExpenseTrackerService>(Reuse.Singleton);
            
            NavigationService.NavigateAsync($"{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(ExpenseListPage)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();            
            Container.RegisterTypeForNavigation<MenuPage>();

            Container.RegisterTypeForNavigation<MainPage>();

            Container.RegisterTypeForNavigation<ExpenseListPage>();
            Container.RegisterTypeForNavigation<ExpenseCreatePage>();
            Container.RegisterTypeForNavigation<ShowGifPage>();
        }
    }
}

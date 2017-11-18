using DryIoc;
using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Settings;
using ExpenseTrackerApp.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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
            Container.Register<IFirebaseService, FirebaseService>(Reuse.Singleton);


            AppCenter.Start($"android={AppSettings.MOBILE_CENTER_KEY};", typeof(Analytics), typeof(Crashes));

            DoInitialNavigate();
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MenuPage>();

            Container.RegisterTypeForNavigation<ExpenseListPage>();
            Container.RegisterTypeForNavigation<ExpenseCreatePage>();
            Container.RegisterTypeForNavigation<ShowGifPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
        }


        private void DoInitialNavigate()
        {
            IUserSettings userSettings = Container.Resolve<IUserSettings>();
            IFirebaseService firebaseService = Container.Resolve<IFirebaseService>();

            if (firebaseService.LoginWithUserSettings(userSettings.GetEmail(), userSettings.GetPassword()))
            {
                NavigationService.NavigateAsync($"ExpenseTrackerApp:///{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(ExpenseListPage)}");
            }
            else
            {
                NavigationService.NavigateAsync($"{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(LoginPage)}");
            }
        }




    }
}

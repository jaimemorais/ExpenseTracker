using DryIoc;
using ExpenseTrackerApp.Helpers;
using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Settings;
using ExpenseTrackerApp.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTrackerApp
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }


        protected override void OnInitialized()
        {
#if DEBUG
            LiveReload.Init();
#endif

            InitializeComponent();

            AppCenter.Start($"android={Secrets.MOBILE_CENTER_KEY};", typeof(Analytics), typeof(Crashes));


            Container.GetContainer().Resolve<IPushService>().Initialize();


            DoInitialNavigate();
        }




        static ITelemetry telemetry = new Telemetry();
        static IUserSettings userSettings = new UserSettings();
        static IHttpConnection httpConnection = new HttpConnection(userSettings, telemetry);
        static IExpenseTrackerService expenseTrackerService = new ExpenseTrackerService(httpConnection, telemetry);
        static IFirebaseService firebaseService = new FirebaseService(userSettings, telemetry);
        static IPushService pushService = new PushService(telemetry, expenseTrackerService);

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {            
            containerRegistry.RegisterInstance<ITelemetry>(telemetry);
            containerRegistry.RegisterInstance<IUserSettings>(userSettings);
            containerRegistry.RegisterInstance<IHttpConnection>(httpConnection);
            containerRegistry.RegisterInstance<IExpenseTrackerService>(expenseTrackerService);
            containerRegistry.RegisterInstance<IFirebaseService>(firebaseService);
            containerRegistry.RegisterInstance<IPushService>(pushService);


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<ExpenseListPage>();
            containerRegistry.RegisterForNavigation<ExpenseCreatePage>();
            containerRegistry.RegisterForNavigation<ShowGifPage>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<SettingsPage>();
        }


        private void DoInitialNavigate()
        {
            IUserSettings userSettings = Container.Resolve<IUserSettings>();
            IFirebaseService firebaseService = Container.Resolve<IFirebaseService>();


            Task.Run(async () =>
            {
                await firebaseService.LoginWithUserSettingsAsync(userSettings.GetEmail(), await userSettings.GetPasswordAsync());
            }).Wait();


            if (firebaseService.GetCurrentUser() != null)
            {
                NavigationService.NavigateAsync($"ExpenseTrackerApp:///{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(ExpenseCreatePage)}");
            }
            else
            {
                NavigationService.NavigateAsync($"{nameof(MenuPage)}/{nameof(NavigationPage)}/{nameof(LoginPage)}");
            }
        }




    }
}

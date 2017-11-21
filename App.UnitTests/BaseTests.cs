using ExpenseTrackerApp.Service;
using ExpenseTrackerApp.Services;
using ExpenseTrackerApp.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Navigation;

namespace App.UnitTests
{
    public class BaseTests
    {

        // TODO use a proper mocking lib

        protected IExpenseTrackerService _expenseTrackerServiceMock;
        protected ITelemetry _telemetryMock;
        protected IUserSettings _userSettingsMock;
        protected INavigationService _navigationServiceMock;



        [TestInitialize]
        public void Initialize()
        {
            _userSettingsMock = new DadosSettingsMock();
            _expenseTrackerServiceMock = new ExpenseTrackerServiceMock();
            _telemetryMock = new TelemetryMock();
            _navigationServiceMock = new NavigationServiceMock();
        }

        
    }
}

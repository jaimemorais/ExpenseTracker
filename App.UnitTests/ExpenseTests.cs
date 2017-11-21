using ExpenseTrackerApp.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.UnitTests
{
    [TestClass]
    public class ExpenseTests : BaseTests
    {
        [TestMethod]
        public void ExpenseSaveSuccess()
        {
            ExpenseCreatePageViewModel expenseCreatePageViewModel = 
                new ExpenseCreatePageViewModel(_expenseTrackerServiceMock, _navigationServiceMock, _userSettingsMock, _telemetryMock);

            // TODO 
            Assert.IsTrue(true);
        }

    }
}

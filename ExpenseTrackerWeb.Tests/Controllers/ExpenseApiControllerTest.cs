using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseTrackerWeb;
using ExpenseTrackerWeb.Controllers;

namespace ExpenseTrackerWeb.Tests.Controllers
{
    [TestClass]
    public class ExpenseApiControllerTest
    {
        [TestMethod]
        public void async Task<List<string>> Get()
        {
            // Arrange
            ExpenseApiController controller = new ExpenseApiController();

            // Act
            IEnumerable<string> result = await controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ExpenseApiController controller = new ExpenseApiController();

            // Act
            string result = controller.Get(5);

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ExpenseApiController controller = new ExpenseApiController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ExpenseApiController controller = new ExpenseApiController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ExpenseApiController controller = new ExpenseApiController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}

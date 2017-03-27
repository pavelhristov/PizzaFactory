using System;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.WebClient.Controllers;
using PizzaFactory.Service.Contracts;
using Moq;

namespace PizzaFactory.Tests.Controllers.PurchaseControllerTests
{
    [TestClass]
    public class Constructor_Should : BaseTest
    {
        [TestMethod]
        public void ReturnAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var userServiceMock = new Mock<IApplicationUserService>();

            PurchaseController controller = new PurchaseController(userServiceMock.Object);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void ThrowException_WhenParametersAreNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PurchaseController(null));
        }
    }
}

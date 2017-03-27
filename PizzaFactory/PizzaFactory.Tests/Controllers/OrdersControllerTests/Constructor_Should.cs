using System;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Service.Contracts;
using Moq;
using PizzaFactory.WebClient.Areas.Administration.Controllers;

namespace PizzaFactory.Tests.Controllers.OrdersControllerTests
{
    [TestClass]
    public class Constructor_Should : BaseTest
    {
        [TestMethod]
        public void ReturnAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var userServiceMock = new Mock<IApplicationUserService>();

            OrdersController controller = new OrdersController(userServiceMock.Object);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void ThrowException_WhenParametersAreNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new OrdersController(null));
        }
    }
}

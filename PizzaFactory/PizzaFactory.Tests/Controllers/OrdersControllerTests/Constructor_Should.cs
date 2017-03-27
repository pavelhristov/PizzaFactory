using System;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Service.Contracts;
using Moq;
using PizzaFactory.WebClient.Areas.Administration.Controllers;
using PizzaFactory.Service.Helpers;

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
            var validatorMock = new Mock<IValidator>();

            OrdersController controller = new OrdersController(userServiceMock.Object, validatorMock.Object);

            // Act & Assert
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void ThrowException_WhenParametersAreNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new OrdersController(null, null));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PizzaFactory.Service.Contracts;
using PizzaFactory.WebClient.Helpers.Contracts;
using PizzaFactory.WebClient.Controllers;

namespace PizzaFactory.Tests.Controllers.PizzaControllerTests
{
    [TestClass]
    public class Constructor_Should : BaseTest
    {
        [TestMethod]
        public void ReturnAnInstance_WhenParametersAreNotNull()
        {
            // Arrange
            var pizzaServiceMock = new Mock<IPizzaService>();
            var ingredientServiceMock = new Mock<IIngredientService>();
            var customPizzaServiceMock = new Mock<ICustomPizzaService>();
            var userServiceMock = new Mock<IApplicationUserService>();
            var cacheProviderMock = new Mock<ICacheProvider>();

            PizzaController controller = new PizzaController(
                pizzaServiceMock.Object,
                ingredientServiceMock.Object,
                customPizzaServiceMock.Object,
                userServiceMock.Object,
                cacheProviderMock.Object);

            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void ThrowException_WhenParametersAreNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PizzaController(null, null, null, null, null));
        }
    }
}

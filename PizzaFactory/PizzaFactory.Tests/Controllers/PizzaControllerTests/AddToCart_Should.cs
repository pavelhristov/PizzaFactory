using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSTestExtensions;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Helpers;
using PizzaFactory.WebClient.Controllers;
using PizzaFactory.WebClient.Helpers.Contracts;
using TestStack.FluentMVCTesting;

namespace PizzaFactory.Tests.Controllers.PizzaControllerTests
{
    [TestClass]
    public class AddToCart_Should : BaseTest
    {
        [TestMethod]
        public void ReturnJsonResult_WhenCalled()
        {
            // Arrange
            var pizzaServiceMock = new Mock<IPizzaService>();
            var ingredientServiceMock = new Mock<IIngredientService>();
            var customPizzaServiceMock = new Mock<ICustomPizzaService>();
            var userServiceMock = new Mock<IApplicationUserService>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var validatorMock = new Mock<IValidator>();


            PizzaController controller = new PizzaController(
                pizzaServiceMock.Object,
                ingredientServiceMock.Object,
                customPizzaServiceMock.Object,
                userServiceMock.Object,
                cacheProviderMock.Object,
                validatorMock.Object);

            // Act & Assert
            controller.WithCallTo(c => c.AddToCart(string.Empty)).ShouldReturnJson();
        }
    }
}

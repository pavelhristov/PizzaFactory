using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Service.Contracts;
using Moq;
using PizzaFactory.WebClient.Helpers.Contracts;
using PizzaFactory.Service.Helpers;
using PizzaFactory.WebClient.Controllers;
using TestStack.FluentMVCTesting;
using PizzaFactory.WebClient.Models;

namespace PizzaFactory.Tests.Controllers.PizzaControllerTests
{
    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        public void ReturnDefaultView_WhenCalledWithHttpGet()
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
            controller.WithCallTo(c => c.Create()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void ReturnRedirectToPizzaChoice_WhenCalledWithHttpPost()
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
            controller.WithCallTo(c => c.Create(new CreateCustomPizzaViewModel())).ShouldRedirectTo<PizzaController>(c => c.Choice());
        }
    }
}

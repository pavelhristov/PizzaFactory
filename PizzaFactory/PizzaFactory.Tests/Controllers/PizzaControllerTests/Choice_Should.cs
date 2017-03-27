using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSTestExtensions;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Service.Models;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.WebClient.Controllers;
using PizzaFactory.WebClient.Helpers.Contracts;
using PizzaFactory.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace PizzaFactory.Tests.Controllers.PizzaControllerTests
{
    [TestClass]
    public class Choice_Should : BaseTest
    {
        [TestMethod]
        public void ReturnDefaultView_WithPizzaViewModel_WhenCalled()
        {
            // Arrange
            var pizzaServiceMock = new Mock<IPizzaService>();
            var ingredientServiceMock = new Mock<IIngredientService>();
            var customPizzaServiceMock = new Mock<ICustomPizzaService>();
            var userServiceMock = new Mock<IApplicationUserService>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var validatorMock = new Mock<IValidator>();

            var pizzas = Helper.GetPizzas().Select(p => new PizzaModel(p));

            pizzaServiceMock.Setup(p => p.GetAll()).Returns(pizzas);
            PizzaController controller = new PizzaController(
                pizzaServiceMock.Object,
                ingredientServiceMock.Object,
                customPizzaServiceMock.Object,
                userServiceMock.Object,
                cacheProviderMock.Object,
                validatorMock.Object);

            // Act & Assert
            controller
                .WithCallTo(c => c.Choice())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<PizzaViewModel>>(vm =>
                {
                    Assert.AreEqual(pizzas.First().Id, vm.First().Id);
                    Assert.AreEqual(pizzas.First().ImgUrl, vm.First().ImgUrl);
                    Assert.AreEqual(pizzas.First().Name, vm.First().Name);
                    Assert.AreEqual(pizzas.First().Price, vm.First().Price);
                    Assert.AreEqual(pizzas.First().Description, vm.First().Description);

                    Assert.AreEqual(pizzas.Last().Id, vm.Last().Id);
                    Assert.AreEqual(pizzas.Last().ImgUrl, vm.Last().ImgUrl);
                    Assert.AreEqual(pizzas.Last().Name, vm.Last().Name);
                    Assert.AreEqual(pizzas.Last().Price, vm.Last().Price);
                    Assert.AreEqual(pizzas.Last().Description, vm.Last().Description);
                });
        }

        [TestMethod]
        public void ReturnDefaultView_WithViewModelWithSameSize_AsServiceResponse()
        {
            // Arrange
            var pizzaServiceMock = new Mock<IPizzaService>();
            var ingredientServiceMock = new Mock<IIngredientService>();
            var customPizzaServiceMock = new Mock<ICustomPizzaService>();
            var userServiceMock = new Mock<IApplicationUserService>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var validatorMock = new Mock<IValidator>();

            var pizzas = Helper.GetPizzas().Select(p => new PizzaModel(p));

            pizzaServiceMock.Setup(p => p.GetAll()).Returns(pizzas);
            PizzaController controller = new PizzaController(
                pizzaServiceMock.Object,
                ingredientServiceMock.Object,
                customPizzaServiceMock.Object,
                userServiceMock.Object,
                cacheProviderMock.Object,
                validatorMock.Object);

            // Act & Assert
            controller
                .WithCallTo(c => c.Choice())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<PizzaViewModel>>(vm =>
                {
                    Assert.AreEqual(pizzas.Count(), vm.Count());
                });
        }
    }
}

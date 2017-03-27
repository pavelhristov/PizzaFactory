using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Service.Contracts;
using Moq;
using PizzaFactory.WebClient.Helpers.Contracts;
using PizzaFactory.Service.Helpers;
using PizzaFactory.WebClient.Controllers;
using TestStack.FluentMVCTesting;
using PagedList;
using PizzaFactory.WebClient.Models;

namespace PizzaFactory.Tests.Controllers.PizzaControllerTests
{
    [TestClass]
    public class Custom_Should : BaseTest
    {
        [TestMethod]
        public void ReturnDefaultView_WithIPagedListOfListCustomPizzaViewModels_WhenCalled()
        {
            // Arrange
            var pizzaServiceMock = new Mock<IPizzaService>();
            var ingredientServiceMock = new Mock<IIngredientService>();
            var customPizzaServiceMock = new Mock<ICustomPizzaService>();
            var userServiceMock = new Mock<IApplicationUserService>();
            var cacheProviderMock = new Mock<ICacheProvider>();
            var validator = new Validator();


            PizzaController controller = new PizzaController(
                pizzaServiceMock.Object,
                ingredientServiceMock.Object,
                customPizzaServiceMock.Object,
                userServiceMock.Object,
                cacheProviderMock.Object,
                validator);

            int page = 1;
            int pageSize = 10;
            // Act & Assert
            controller
                .WithCallTo(c => c.Custom(page, pageSize))
                .ShouldRenderDefaultView()
                .WithModel<IPagedList<ListCustomPizzaViewModel>>();
        }
    }
}

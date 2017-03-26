using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using MSTestExtensions;
using PizzaFactory.Data;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Data.Models;
using Moq;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Service.Models;

namespace PizzaFactory.Tests.Services.CustomPizzaServiceTests
{
    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        public void CallMapperFromCreateCustomPizzaModel_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = new List<CustomPizza>();
            CustomPizza customPizza = new CustomPizza();
            CreateCustomPizzaModel ccpm = new CreateCustomPizzaModel();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            mapperMock.Setup(m => m.FromCreateCustomPizzaModel(contextMock.Object, ccpm)).Returns(customPizza);
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);

            // Act
            customPizzaService.Create(ccpm);

            // Assert
            mapperMock.Verify(m => m.FromCreateCustomPizzaModel(contextMock.Object, ccpm), Times.Once);
        }


        [TestMethod]
        public void AddNewCustomPizzaToCustomPizzas_WhenCalled()
        {
            // Arrange
            ICollection<CustomPizza> pizzas = new List<CustomPizza>();
            CustomPizza customPizza = new CustomPizza();
            CreateCustomPizzaModel ccpm = new CreateCustomPizzaModel();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            mapperMock.Setup(m => m.FromCreateCustomPizzaModel(contextMock.Object, ccpm)).Returns(customPizza);
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas as IEnumerable<CustomPizza>);
            pizzaDbSetMock.Setup(p => p.Add(customPizza)).Callback(() => pizzas.Add(customPizza));
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);

            // Act
            customPizzaService.Create(ccpm);

            // Assert
            Assert.AreEqual(1, pizzas.Count());
        }

        [TestMethod]
        public void ReturnPositiveNumber_IfPizzaWasCreated_WhenCalled()
        {
            // Arrange
            ICollection<CustomPizza> pizzas = new List<CustomPizza>();
            CustomPizza customPizza = new CustomPizza();
            CreateCustomPizzaModel ccpm = new CreateCustomPizzaModel();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            mapperMock.Setup(m => m.FromCreateCustomPizzaModel(contextMock.Object, ccpm)).Returns(customPizza);
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas as IEnumerable<CustomPizza>);
            pizzaDbSetMock.Setup(p => p.Add(customPizza)).Callback(() => pizzas.Add(customPizza));
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            contextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => pizzas.Count > 0 ? 1 : 0);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);

            // Act
            int result = customPizzaService.Create(ccpm);

            // Assert
            Assert.IsTrue(result > 0);
        }


        [TestMethod]
        public void ReturnZero_IfPizzaWasNotCreated_WhenCalled()
        {
            // Arrange
            ICollection<CustomPizza> pizzas = new List<CustomPizza>();
            CustomPizza customPizza = new CustomPizza();
            CreateCustomPizzaModel ccpm = new CreateCustomPizzaModel();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            mapperMock.Setup(m => m.FromCreateCustomPizzaModel(contextMock.Object, ccpm)).Returns(customPizza);
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas as IEnumerable<CustomPizza>);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            contextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => pizzas.Count > 0 ? 1 : 0);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);

            // Act
            int result = customPizzaService.Create(ccpm);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}

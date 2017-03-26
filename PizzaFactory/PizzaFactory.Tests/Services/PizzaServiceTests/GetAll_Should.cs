using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Data;
using Moq;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Models;
using System.Linq;

namespace PizzaFactory.Tests.Services.PizzaServiceTests
{
    /// <summary>
    /// Summary description for GetAll_Should
    /// </summary>
    [TestClass]
    public class GetAll_Should : BaseTest
    {
        [TestMethod]
        public void ReturnIEnumerableOfPizzaModels_WhenCalled()
        {
            // Arrange
            var pizzas = Helper.GetPizzas();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            IPizzaService pizzaService = new PizzaService(contextMock.Object);

            // Act
            var pizzasFound = pizzaService.GetAll();

            // Assert
            Assert.IsInstanceOfType(pizzasFound, typeof(IEnumerable<PizzaModel>));
        }

        [TestMethod]
        public void ReturnAllPizzas_WhenCalled()
        {
            // Arrange
            var pizzas = Helper.GetPizzas();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            IPizzaService pizzaService = new PizzaService(contextMock.Object);

            // Act
            var pizzasFound = pizzaService.GetAll();

            // Assert
            Assert.AreEqual(pizzas.Count(), pizzasFound.Count());
        }
    }
}

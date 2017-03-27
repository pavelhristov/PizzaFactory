using System;
using System.Collections.Generic;
using System.Linq;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Data.Models;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Data;
using PizzaFactory.Service.Helpers;
using Moq;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service;

namespace PizzaFactory.Tests.Services.ApplicationUserServiceTests
{
    [TestClass]
    public class RemoveFromCart_Should : BaseTest
    {
        [TestMethod]
        public void ReturnZero_IfPizzaWasNotFound_WhenCalled()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            var user = new ApplicationUser()
            {
                Cart = new List<BasePizza>()
                {
                    new BasePizza()
                }
            };
            IEnumerable<ApplicationUser> users = new List<ApplicationUser>()
            {
                user
            };
            IEnumerable<Pizza> pizzas = Helper.GetPizzas();
            IEnumerable<CustomPizza> customPizzas = Helper.GetCustomPizzas();

            var pizzaContextMock = new Mock<IPizzaFactoryDbContext>();
            var orderContextMock = new Mock<IOrderDbContext>();
            var userContextMock = new Mock<IIdentityDbContext>() { CallBase = true };
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            var customPizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(customPizzas);
            var userDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(users);

            userDbSetMock.Setup(u => u.Find(userId.ToString())).Returns(user);
            userContextMock.Setup(ctx => ctx.Users).Returns(userDbSetMock.Object);
            userContextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => user.Cart.Count == 1 ? 0 : 1);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            int result = userService.RemoveFromCart(userId.ToString(), pizzas.First().Id);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ReturnPositiveNumber_IfPizzaWasFoundAndSuccessfullyRemoved_WhenCalled()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Guid pizzaId = Guid.NewGuid();

            var user = new ApplicationUser()
            {
                Cart = new List<BasePizza>()
                {
                    new BasePizza()
                    {
                        Id = pizzaId
                    }
                }
            };
            IEnumerable<ApplicationUser> users = new List<ApplicationUser>()
            {
                user
            };
            IEnumerable<Pizza> pizzas = Helper.GetPizzas();
            IEnumerable<CustomPizza> customPizzas = Helper.GetCustomPizzas();

            var pizzaContextMock = new Mock<IPizzaFactoryDbContext>();
            var orderContextMock = new Mock<IOrderDbContext>();
            var userContextMock = new Mock<IIdentityDbContext>() { CallBase = true };
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            var customPizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(customPizzas);
            var userDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(users);

            userDbSetMock.Setup(u => u.Find(userId.ToString())).Returns(user);
            userContextMock.Setup(ctx => ctx.Users).Returns(userDbSetMock.Object);
            userContextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => user.Cart.Count == 1 ? 0 : 1);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            int result = userService.RemoveFromCart(userId.ToString(), pizzaId);

            // Assert
            Assert.IsTrue(result > 0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Data.Models;
using Moq;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Data;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service;

namespace PizzaFactory.Tests.Services.ApplicationUserServiceTests
{
    [TestClass]
    public class AddToCart_Should : BaseTest
    {
        [TestMethod]
        public void CallMapperFromPizzaAndCustomPizza_WhenCalled()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            var user = new ApplicationUser()
            {
                Cart = new List<BasePizza>()
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
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            userService.AddToCart(userId.ToString(), pizzas.First().Id);

            // Assert
            mapperMock.Verify(v => v.FromPizzaAndCustomPizza(It.IsAny<Pizza>(), It.IsAny<CustomPizza>()), Times.Once);
        }

        [TestMethod]
        public void AddBasePizzaToUserCart_WhenCalled()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            var user = new ApplicationUser()
            {
                Cart = new List<BasePizza>()
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
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            userService.AddToCart(userId.ToString(), pizzas.First().Id);

            // Assert
            Assert.AreEqual(1, user.Cart.Count);
        }

        [TestMethod]
        public void ReturnPositiveNumber_IfPizzaWasSuccessfullyAddedToCart_WhenCalled()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            var user = new ApplicationUser()
            {
                Cart = new List<BasePizza>()
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
            userContextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => user.Cart.Count > 0 ? 1 : 0);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            int result = userService.AddToCart(userId.ToString(), pizzas.First().Id);

            // Assert
            Assert.IsTrue(result > 0);
        }


        [TestMethod]
        public void ReturnZero_IfPizzaWasNotSuccessfullyAddedToCart_WhenCalled()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            var user = new ApplicationUser()
            {
                Cart = new List<BasePizza>()
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
            userContextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => 0);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            int result = userService.AddToCart(userId.ToString(), pizzas.First().Id);

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}

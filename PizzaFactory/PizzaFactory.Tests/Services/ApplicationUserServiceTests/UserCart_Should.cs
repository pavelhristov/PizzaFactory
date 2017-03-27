using System;
using System.Collections.Generic;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Data.Models;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Data;
using Moq;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service;
using PizzaFactory.Service.Models;
using System.Linq;

namespace PizzaFactory.Tests.Services.ApplicationUserServiceTests
{
    [TestClass]
    public class UserCart_Should : BaseTest
    {
        [TestMethod]
        public void CallMapperFromOrders_WhenCalled()
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
            IEnumerable<Order> orders = new List<Order>();

            var pizzaContextMock = new Mock<IPizzaFactoryDbContext>();
            var orderContextMock = new Mock<IOrderDbContext>();
            var userContextMock = new Mock<IIdentityDbContext>() { CallBase = true };
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            var customPizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(customPizzas);
            var userDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(users);
            var orderDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(orders);

            userDbSetMock.Setup(u => u.Find(userId.ToString())).Returns(user);
            userContextMock.Setup(ctx => ctx.Users).Returns(userDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);
            orderContextMock.Setup(ctx => ctx.Orders).Returns(orderDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);

            // Act
            var result = userService.UserCart(userId.ToString());

            // Assert
            mapperMock.Verify(m => m.FromBasePizzas(It.IsAny<IEnumerable<BasePizza>>()), Times.Once);
        }

        [TestMethod]
        public void ReturnIEnumerableOfBasePizzaModel_WhenCalled()
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
            IEnumerable<Order> orders = new List<Order>();

            var pizzaContextMock = new Mock<IPizzaFactoryDbContext>();
            var orderContextMock = new Mock<IOrderDbContext>();
            var userContextMock = new Mock<IIdentityDbContext>() { CallBase = true };
            var mapper = new Mapper();
            var validator = new Validator();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            var customPizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(customPizzas);
            var userDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(users);
            var orderDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(orders);

            userDbSetMock.Setup(u => u.Find(userId.ToString())).Returns(user);
            userContextMock.Setup(ctx => ctx.Users).Returns(userDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);
            orderContextMock.Setup(ctx => ctx.Orders).Returns(orderDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapper,
                validator);

            // Act
            var result = userService.UserCart(userId.ToString());

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<BasePizzaModel>));
        }

        [TestMethod]
        public void ReturnCorrectNumberOfPizzas_WhenCalled()
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
            IEnumerable<Order> orders = new List<Order>();

            var pizzaContextMock = new Mock<IPizzaFactoryDbContext>();
            var orderContextMock = new Mock<IOrderDbContext>();
            var userContextMock = new Mock<IIdentityDbContext>() { CallBase = true };
            var mapper = new Mapper();
            var validator = new Validator();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            var customPizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(customPizzas);
            var userDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(users);
            var orderDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(orders);

            userDbSetMock.Setup(u => u.Find(userId.ToString())).Returns(user);
            userContextMock.Setup(ctx => ctx.Users).Returns(userDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);
            orderContextMock.Setup(ctx => ctx.Orders).Returns(orderDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapper,
                validator);

            // Act
            var result = userService.UserCart(userId.ToString());

            // Assert
            Assert.AreEqual(user.Cart.Count, result.Count());
        }
    }
}

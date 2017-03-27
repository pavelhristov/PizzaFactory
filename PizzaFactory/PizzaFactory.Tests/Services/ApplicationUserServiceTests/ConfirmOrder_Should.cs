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

namespace PizzaFactory.Tests.Services.ApplicationUserServiceTests
{
    [TestClass]
    public class ConfirmOrder_Should : BaseTest
    {
        [TestMethod]
        public void ReturnPositiveNumber_IfOrderWasSuccessfullyCreated()
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
            IEnumerable<Order> orders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Today,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                },
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                }
            };

            var pizzaContextMock = new Mock<IPizzaFactoryDbContext>();
            var orderContextMock = new Mock<IOrderDbContext>();
            var userContextMock = new Mock<IIdentityDbContext>() { CallBase = true };
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            var customPizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(customPizzas);
            var userDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(users);
            var orderDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(orders);

            bool isOrderAdded = false;
            orderDbSetMock.Setup(o => o.Add(It.IsAny<Order>())).Callback(() => isOrderAdded = true);
            userDbSetMock.Setup(u => u.Find(userId.ToString())).Returns(user);
            userContextMock.Setup(ctx => ctx.Users).Returns(userDbSetMock.Object);
            orderContextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => isOrderAdded ? 1 : 0);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);
            orderContextMock.Setup(ctx => ctx.Orders).Returns(orderDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);
            string address = "random address";

            // Act
            int result = userService.ConfirmOrder(userId.ToString(), address);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ReturnPositiveNumber_IfUsersCartWasSuccessfullyEmptied()
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
            IEnumerable<Order> orders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Today,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                },
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                }
            };

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
            orderContextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => user.Cart.Count == 0 ? 1 : 0);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);
            orderContextMock.Setup(ctx => ctx.Orders).Returns(orderDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);
            string address = "random address";

            // Act
            int result = userService.ConfirmOrder(userId.ToString(), address);

            // Assert
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void ReturnZero_IfOrderWasNotSuccessfullyCreated()
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
            IEnumerable<Order> orders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Today,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                },
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                }
            };

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
            orderContextMock.Setup(ctx => ctx.SaveChanges()).Returns(() => 0);
            pizzaContextMock.Setup(ctx => ctx.Pizzas).Returns(pizzaDbSetMock.Object);
            pizzaContextMock.Setup(ctx => ctx.CustomPizzas).Returns(customPizzaDbSetMock.Object);
            orderContextMock.Setup(ctx => ctx.Orders).Returns(orderDbSetMock.Object);

            IApplicationUserService userService =
                new ApplicationUserService(userContextMock.Object,
                pizzaContextMock.Object,
                orderContextMock.Object,
                mapperMock.Object,
                validatorMock.Object);
            string address = "random address";

            // Act
            int result = userService.ConfirmOrder(userId.ToString(), address);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void EmptyUserCart_WhenCalled()
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
            IEnumerable<Order> orders = new List<Order>()
            {
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Today,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                },
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.Now,
                    Pizzas = new List<BasePizza>(),
                    Customer = new ApplicationUser()
                }
            };

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
            string address = "random address";

            // Act
            userService.ConfirmOrder(userId.ToString(), address);

            // Assert
            Assert.AreEqual(0, user.Cart.Count);
        }
    }
}

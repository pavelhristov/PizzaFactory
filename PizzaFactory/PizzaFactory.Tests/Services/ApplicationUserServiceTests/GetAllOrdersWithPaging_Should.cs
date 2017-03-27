using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Data.Models;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Data;
using Moq;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Service;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Models;

namespace PizzaFactory.Tests.Services.ApplicationUserServiceTests
{
    [TestClass]
    public class GetAllOrdersWithPaging_Should : BaseTest
    {
        [TestMethod]
        public void CallValidatorValidatePage_WhenCalled()
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
            int count = 0;

            // Act
            var result = userService.GetAllOrdersWithPaging(out count);

            // Assert
            validatorMock.Verify(v => v.ValidatePage(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void CallValidatorValidatePageSize_WhenCalled()
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
            int count = 0;

            // Act
            var result = userService.GetAllOrdersWithPaging(out count);

            // Assert
            validatorMock.Verify(v => v.ValidatePageSize(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

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
            int count = 0;

            // Act
            var result = userService.GetAllOrdersWithPaging(out count);

            // Assert
            mapperMock.Verify(m => m.FromOrders(It.IsAny<IEnumerable<Order>>()), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectCollectionCount_AsOutParameter_WhenCalled()
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
            int count = 0;

            // Act
            var result = userService.GetAllOrdersWithPaging(out count);

            // Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void ReturnIEnumerableOfOrderModels_WhenCalled()
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
            int count = 0;

            // Act
            var result = userService.GetAllOrdersWithPaging(out count);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<OrderModel>));
        }

        [TestMethod]
        public void ReturnCollection_ForTheProvidedPage_OrderedByCreatedOnDescending_WhenCalled()
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
            int count = 0;
            int pageSize = 1;
            int page = 2;

            // Act
            var result = userService.GetAllOrdersWithPaging(out count, page, pageSize);

            // Assert
            Assert.AreEqual(orders.OrderByDescending(x => x.CreatedOn).ToList()[page - 1].CreatedOn, result.First().CreatedOn);
        }

        [TestMethod]
        public void ReturnCollection_WithPageSizeNumberOfItems_WhenCalled()
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
            int count = 0;
            int pageSize = 1;
            int page = 2;

            // Act
            var result = userService.GetAllOrdersWithPaging(out count, page, pageSize);

            // Assert
            Assert.AreEqual(pageSize, result.Count());
        }
    }
}

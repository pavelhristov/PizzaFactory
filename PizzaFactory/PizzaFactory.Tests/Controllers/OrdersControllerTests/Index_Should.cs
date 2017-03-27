using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSTestExtensions;
using PagedList;
using PizzaFactory.Data;
using PizzaFactory.Data.Models;
using PizzaFactory.Service;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.WebClient.Areas.Administration.Controllers;
using PizzaFactory.WebClient.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace PizzaFactory.Tests.Controllers.OrdersControllerTests
{
    [TestClass]
    public class Index_Should : BaseTest
    {
        [TestMethod]
        public void ReturnDefaultView_WithIPagedListOfOrderViewModels_WhenCalled()
        {
            // Arrange
            var userServiceMock = new Mock<IApplicationUserService>();
            var validator = new Validator();

            OrdersController controller = new OrdersController(userServiceMock.Object, validator);

            // Act & Assert
            controller
                .WithCallTo(c => c.Index(1, 10))
                .ShouldRenderDefaultView()
                .WithModel<StaticPagedList<OrderViewModel>>();
        }

        [TestMethod]
        public void ReturnDefaultView_WithIPagedListOfOrderViewModels_WithSpecifiedPageSize_WhenCalled()
        {
            // Arrange
            IEnumerable<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser()
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

            OrdersController controller = new OrdersController(userService, validator);
            int page = 1;
            int pageSize = 1;

            // Act & Assert
            controller
                .WithCallTo(c => c.Index(page, pageSize))
                .ShouldRenderDefaultView()
                .WithModel<StaticPagedList<OrderViewModel>>(vm =>
                {
                    Assert.AreEqual(pageSize, vm.Count);
                });
        }
    }
}

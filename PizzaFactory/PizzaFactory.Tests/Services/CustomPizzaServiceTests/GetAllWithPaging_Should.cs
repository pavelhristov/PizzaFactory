using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTestExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaFactory.Data.Models;
using Moq;
using PizzaFactory.Data;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service;
using PizzaFactory.Service.Models;

namespace PizzaFactory.Tests.Services.CustomPizzaServiceTests
{
    [TestClass]
    public class GetAllWithPaging_Should : BaseTest
    {
        [TestMethod]
        public void CallValidatePage_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = new List<CustomPizza>();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);
            int count = 0;

            // Act
            customPizzaService.GetAllWithPaging(out count);

            // Assert
            validatorMock.Verify(v => v.ValidatePage(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void CallValidatePageSize_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = new List<CustomPizza>();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);
            int count = 0;

            // Act
            customPizzaService.GetAllWithPaging(out count);

            // Assert
            validatorMock.Verify(v => v.ValidatePageSize(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void CallMapperFromCustomPizzas_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = new List<CustomPizza>();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);
            int count = 0;

            // Act
            customPizzaService.GetAllWithPaging(out count);

            // Assert
            mapperMock.Verify(m => m.FromCustomPizzas(It.IsAny<ICollection<CustomPizza>>()), Times.Once);
        }

        [TestMethod]
        public void ReturnCorrectCollectionCount_AsOutParameter_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = Helper.GetCustomPizzas();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapperMock = new Mock<IMapper>();
            var validatorMock = new Mock<IValidator>();
            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapperMock.Object, validatorMock.Object);
            int count = 0;

            // Act
            customPizzaService.GetAllWithPaging(out count);

            // Assert
            Assert.AreEqual(pizzas.Count(), count);
        }


        [TestMethod]
        public void ReturnCollection_WithPageSizeNumberOfItems_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = Helper.GetCustomPizzas();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapper = new Mapper();
            var validator = new Validator();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapper, validator);

            int count = 0;
            int pageSize = 1;
            int page = 1;

            // Act
            var result = customPizzaService.GetAllWithPaging(out count, page, pageSize);

            // Assert
            Assert.AreEqual(pageSize, result.Count());
        }

        [TestMethod]
        public void ReturnCollection_WithForTheProvidedPage_OrderedByName_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = Helper.GetCustomPizzas();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapper = new Mapper();
            var validator = new Validator();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapper, validator);

            int count = 0;
            int pageSize = 1;
            int page = 2;

            // Act
            var result = customPizzaService.GetAllWithPaging(out count, page, pageSize);

            // Assert
            Assert.AreEqual(pizzas.OrderBy(x => x.Name).ToList()[page - 1].Name, result.First().Name);
        }

        [TestMethod]
        public void ReturnIEnumerableOfCustomPizzaModels_WhenCalled()
        {
            // Arrange
            IEnumerable<CustomPizza> pizzas = Helper.GetCustomPizzas();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var mapper = new Mapper();
            var validator = new Validator();

            var pizzaDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(pizzas);
            contextMock.Setup(ctx => ctx.CustomPizzas).Returns(pizzaDbSetMock.Object);
            ICustomPizzaService customPizzaService = new CustomPizzaService(contextMock.Object, mapper, validator);

            int count = 0;

            // Act
            var result = customPizzaService.GetAllWithPaging(out count);

            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<CustomPizzaModel>));
        }
    }
}

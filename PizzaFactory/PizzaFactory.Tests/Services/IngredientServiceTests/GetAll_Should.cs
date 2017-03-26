using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTestExtensions;
using PizzaFactory.Tests.Helpers;
using PizzaFactory.Data;
using Moq;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service;
using PizzaFactory.Service.Models;

namespace PizzaFactory.Tests.Services.IngredientServiceTests
{
    [TestClass]
    public class GetAll_Should : BaseTest
    {
        [TestMethod]
        public void ReturnIEnumerableOfIngredientModels_WhenCalled()
        {
            // Arrange
            var ingredients = Helper.GetIngredients();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var ingredientsDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(ingredients);
            contextMock.Setup(ctx => ctx.Ingredients).Returns(ingredientsDbSetMock.Object);
            IIngredientService ingredientService = new IngredientService(contextMock.Object);

            // Act
            var ingredientsFound = ingredientService.GetAll();

            // Assert
            Assert.IsInstanceOfType(ingredientsFound, typeof(IEnumerable<IngredientModel>));
        }

        [TestMethod]
        public void ReturnAllIngredients_WhenCalled()
        {
            // Arrange
            var ingredients = Helper.GetIngredients();
            var contextMock = new Mock<IPizzaFactoryDbContext>();
            var ingredientsDbSetMock = QueryableDbSetMock.GetQueryableMockDbSet(ingredients);
            contextMock.Setup(ctx => ctx.Ingredients).Returns(ingredientsDbSetMock.Object);
            IIngredientService ingredientService = new IngredientService(contextMock.Object);

            // Act
            var ingredientsFound = ingredientService.GetAll();

            // Assert
            Assert.AreEqual(ingredients.Count(), ingredientsFound.Count());
        }
    }
}

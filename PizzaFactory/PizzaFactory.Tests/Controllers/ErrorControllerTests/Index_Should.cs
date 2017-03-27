using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using PizzaFactory.WebClient.Controllers;
using TestStack.FluentMVCTesting;

namespace PizzaFactory.Tests.Controllers.ErrorControllerTests
{
    [TestClass]
    public class Index_Should : BaseTest
    {
        [TestMethod]
        public void ReturnErrorView_WhenCalled()
        {
            // Arrange
            ErrorController controller = new ErrorController();

            // Act & Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderView("Error");
        }
    }
}

using Bytes2you.Validation;
using PagedList;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Helpers;
using PizzaFactory.WebClient.Areas.Administration.Models;
using PizzaFactory.WebClient.Attributes;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Areas.Administration.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private IApplicationUserService userService;
        private IValidator validator;

        public OrdersController(IApplicationUserService userService, IValidator validator)
        {
            Guard.WhenArgument(userService, nameof(userService)).IsNull().Throw();
            Guard.WhenArgument(validator, nameof(validator)).IsNull().Throw();

            this.userService = userService;
            this.validator = validator;
        }

        // GET: Administration/Orders
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            page = this.validator.ValidatePage(page);
            pageSize = this.validator.ValidatePageSize(pageSize);

            int count = 0;
            var orders = this.userService.GetAllOrdersWithPaging(out count, page, pageSize);
            var orderViewModels = new List<OrderViewModel>();

            foreach (var item in orders)
            {
                var orderViewModel = new OrderViewModel(item.Id, item.User, item.Pizzas, item.Price, item.CreatedOn, item.Address);
                orderViewModels.Add(orderViewModel);
            }

            return View(new StaticPagedList<OrderViewModel>(orderViewModels, page, pageSize, count));
        }
    }
}
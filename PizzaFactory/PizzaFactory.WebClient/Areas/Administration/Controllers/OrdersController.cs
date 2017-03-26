using PagedList;
using PizzaFactory.Service.Contracts;
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

        public OrdersController(IApplicationUserService userService)
        {
            this.userService = userService;
        }

        // GET: Administration/Orders
        public ActionResult Index(int page = 1, int pageSize = 10)
        {

            if (page < 1)
            {
                page = 1;
            }

            if (pageSize < 1)
            {
                pageSize = 1;
            }

            if (pageSize > 10)
            {
                pageSize = 10;
            }

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
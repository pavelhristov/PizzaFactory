using Microsoft.AspNet.Identity;
using PizzaFactory.Service.Contracts;
using PizzaFactory.WebClient.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private IApplicationUserService userService;

        public PurchaseController(IApplicationUserService userService)
        {
            this.userService = userService;
        }

        // GET: Purchase
        public ActionResult Index()
        {
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            var pizzaList = this.userService.UserCart(userId);
            var pizzaModelList = new List<BasePizzaViewModel>();

            foreach (var item in pizzaList)
            {
                pizzaModelList.Add(new BasePizzaViewModel()
                {
                    Name = item.Name,
                    Price = item.Price
                });
            }

            return View(pizzaModelList);
        }

        public JsonResult ConfirmOrder(string address)
        {
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();



            return Json(new { message = "Successful order!", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
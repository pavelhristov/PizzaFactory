using Microsoft.AspNet.Identity;
using PizzaFactory.Service.Contracts;
using PizzaFactory.WebClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
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


            int isSaved = 0;

            Task responseTask = Task.Run(() =>
            {
                isSaved = this.userService.ConfirmOrder(userId, address);
            });
            responseTask.Wait();

            if (isSaved>0)
            {
                return Json(new { message = "Successful order!", success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Order failed", success = false }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}
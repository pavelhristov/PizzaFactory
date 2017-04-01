using Bytes2you.Validation;
using Microsoft.AspNet.Identity.Owin;
using PizzaFactory.Authentication;
using PizzaFactory.Service.Contracts;
using PizzaFactory.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Areas.Api.Controllers
{
    public class PizzasController : Controller
    {
        private ICustomPizzaService customPizzaService;
        private IPizzaService pizzaService;
        private IApplicationUserService userService;

        public PizzasController(IPizzaService pizzaService, ICustomPizzaService customPizzaService, IApplicationUserService userService)
        {
            Guard.WhenArgument(pizzaService, nameof(pizzaService)).IsNull().Throw();
            Guard.WhenArgument(customPizzaService, nameof(customPizzaService)).IsNull().Throw();
            Guard.WhenArgument(userService, nameof(userService)).IsNull().Throw();

            this.pizzaService = pizzaService;
            this.customPizzaService = customPizzaService;
            this.userService = userService;
        }

        // GET: Api/Pizzas
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Ours()
        {
            var pizzas = this.pizzaService.GetAll();

            return Json(new { data = pizzas, success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OursById(string id)
        {
            var pizza = this.pizzaService.GetById(Guid.Parse(id));

            return Json(new { pizza }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Custom()
        {
            var pizzas = this.customPizzaService.GetAll();
            var pizzaList = new List<ListCustomPizzaViewModel>();

            foreach (var item in pizzas)
            {
                pizzaList.Add(new ListCustomPizzaViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Ingredients = string.Join(", ", item.Ingredients.Select(i => i.Name).ToList()),
                    Price = item.Price
                });
            }

            return Json(new { data = pizzaList, success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CustomById(string id)
        {
            var customPizza = this.customPizzaService.GetById(Guid.Parse(id));

            var customPizzaViewModel = new ListCustomPizzaViewModel()
            {
                Id = customPizza.Id,
                Name = customPizza.Name,
                Description = customPizza.Description,
                Ingredients = string.Join(", ", customPizza.Ingredients.Select(i => i.Name).ToList()),
                Price = customPizza.Price
            };

            return Json(new { data = customPizzaViewModel, success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddToCart(string userId, string pizzaId)
        {
            int isSaved = 0;

            Task responseTask = Task.Run(() =>
            {
                isSaved = this.userService.AddToCart(userId, Guid.Parse(pizzaId));
            });

            responseTask.Wait();

            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}
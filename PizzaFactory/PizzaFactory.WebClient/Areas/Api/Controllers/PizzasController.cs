using PizzaFactory.Service.Contracts;
using PizzaFactory.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Areas.Api.Controllers
{
    [Authorize]
    public class PizzasController : Controller
    {
        private ICustomPizzaService customPizzaService;
        private IPizzaService pizzaService;

        public PizzasController(IPizzaService pizzaService, ICustomPizzaService customPizzaService)
        {
            this.pizzaService = pizzaService;
            this.customPizzaService = customPizzaService;
        }

        // GET: Api/Pizzas
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public JsonResult Ours()
        {
            var pizzas = this.pizzaService.GetAll();

            return Json(new { data = pizzas, success = true }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
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
    }
}
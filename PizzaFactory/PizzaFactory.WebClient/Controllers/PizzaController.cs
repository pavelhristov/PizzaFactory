using PizzaFactory.Service;
using PizzaFactory.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        private IPizzaService pizzaService;
        
        public PizzaController(IPizzaService pizzaService)
        {
            this.pizzaService = pizzaService;
        }
        
        [AllowAnonymous]
        public ActionResult Choice()
        {
            var pizzas = this.pizzaService.GetAll().ToList();
            return View(pizzas);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name)
        {
            return Redirect("~/Pizza/Choice");
        }
    }
}
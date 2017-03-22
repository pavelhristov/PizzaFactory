using Bytes2you.Validation;
using PizzaFactory.Service;
using PizzaFactory.Service.Contracts;
using PizzaFactory.WebClient.Models;
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
            Guard.WhenArgument(pizzaService, nameof(pizzaService)).IsNull().Throw();

            this.pizzaService = pizzaService;
        }
        
        [AllowAnonymous]
        public ActionResult Choice()
        {
            var pizzaModels = new List<PizzaViewModel>();
            var pizzas = this.pizzaService.GetAll();

            // TODO: move to factory or automapper or expression or whatever
            foreach (var item in pizzas)
            {
                pizzaModels.Add(
                new PizzaViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImgUrl = item.ImgUrl
                });
            }

            return View(pizzaModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name)
        {
            //this.pizzaService.Create(Name);
            
            return Redirect("~/Pizza/Choice");
        }
    }
}
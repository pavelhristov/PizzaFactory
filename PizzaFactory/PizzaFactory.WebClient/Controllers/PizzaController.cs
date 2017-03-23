using Bytes2you.Validation;
using PizzaFactory.Service;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Models;
using PizzaFactory.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Controllers
{
    //[Authorize]
    public class PizzaController : Controller
    {
        private IPizzaService pizzaService;
        private IIngredientService ingredientService;
        private ICustomPizzaService customPizzaService;


        public PizzaController(IPizzaService pizzaService, IIngredientService ingredientService, ICustomPizzaService customPizzaService)
        {
            Guard.WhenArgument(pizzaService, nameof(pizzaService)).IsNull().Throw();
            Guard.WhenArgument(ingredientService, nameof(ingredientService)).IsNull().Throw();
            Guard.WhenArgument(customPizzaService, nameof(customPizzaService)).IsNull().Throw();

            this.ingredientService = ingredientService;
            this.pizzaService = pizzaService;
            this.customPizzaService = customPizzaService;
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
            var ingredients = this.ingredientService.GetAll().ToList();
            ViewBag.Items = new SelectList(ingredients, "ID", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateCustomPizzaViewModel pizza)
        {
            this.customPizzaService.Create(new CreateCustomPizzaModel()
            {
                Name = pizza.Name,
                Description = pizza.Description,
                Ingredients = pizza.Ingredients
            });

            return Redirect("~/Pizza/Choice");
        }

        [HttpGet]
        public ActionResult Custom()
        {
            var pizzas = this.customPizzaService.GetAll();
            var pizzaList = new List<ListCustomPizzaViewModel>();

            foreach (var item in pizzas)
            {
                pizzaList.Add(new ListCustomPizzaViewModel()
                {
                    Name = item.Name,
                    Description = item.Description,
                    Ingredients = string.Join(", ", item.Ingredients.Select(i => i.Name).ToList()),
                    Price = item.Price
                });
            }

            return this.View(pizzaList);
        }
    }
}
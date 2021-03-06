﻿using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using PizzaFactory.Authentication;
using PizzaFactory.Service;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Helpers;
using PizzaFactory.Service.Models;
using PizzaFactory.WebClient.Helpers.Contracts;
using PizzaFactory.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PizzaFactory.WebClient.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        private IPizzaService pizzaService;
        private IIngredientService ingredientService;
        private ICustomPizzaService customPizzaService;
        private IApplicationUserService userService;
        private ICacheProvider cacheProvider;
        private IValidator validator;

        public PizzaController(IPizzaService pizzaService,
            IIngredientService ingredientService,
            ICustomPizzaService customPizzaService,
            IApplicationUserService userService,
            ICacheProvider cacheProvider,
            IValidator validator)
        {
            Guard.WhenArgument(pizzaService, nameof(pizzaService)).IsNull().Throw();
            Guard.WhenArgument(ingredientService, nameof(ingredientService)).IsNull().Throw();
            Guard.WhenArgument(customPizzaService, nameof(customPizzaService)).IsNull().Throw();
            Guard.WhenArgument(userService, nameof(userService)).IsNull().Throw();
            Guard.WhenArgument(cacheProvider, nameof(cacheProvider)).IsNull().Throw();
            Guard.WhenArgument(validator, nameof(validator)).IsNull().Throw();

            this.ingredientService = ingredientService;
            this.pizzaService = pizzaService;
            this.customPizzaService = customPizzaService;
            this.userService = userService;
            this.cacheProvider = cacheProvider;
            this.validator = validator;
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

            return this.View(pizzaModels);
        }

        [HttpGet]
        public ActionResult Create()
        {
            string ingredientsKey = "ingredients";

            var ingredients = this.cacheProvider.GetItem(ingredientsKey) as IEnumerable<IngredientModel>;

            if (ingredients == null || ingredients.Count() <= 0)
            {
                ingredients = this.ingredientService.GetAll();
                this.cacheProvider.SetItem(ingredientsKey, ingredients);
            }

            ViewBag.Items = new SelectList(ingredients, "ID", "Name");

            return this.View();
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

            return this.RedirectToAction("Choice");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Custom(int page = 1, int pageSize = 10)
        {
            page = this.validator.ValidatePage(page);
            pageSize = this.validator.ValidatePageSize(pageSize);

            int count;

            var pizzas = this.customPizzaService.GetAllWithPaging(out count, page, pageSize, cp => cp.Price);
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

            //var model = pizzaList.ToPagedList(page, pageSize);
            var model = new StaticPagedList<ListCustomPizzaViewModel>(pizzaList, page, pageSize, count);

            return this.View(model);
        }

        [AllowAnonymous]
        [ChildActionOnly]
        [HttpGet]
        public PartialViewResult CreatePizza()
        {
            if (User.Identity.IsAuthenticated)
            {
                return this.PartialView();
            }

            return null;
        }

        public JsonResult AddToCart(string productId)
        {
            if (string.IsNullOrWhiteSpace(productId))
            {
                return this.Json(new { message = "Invalid request!", success = false }, JsonRequestBehavior.AllowGet);
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Json(new { message = "You must log in to make an order!", success = false }, JsonRequestBehavior.AllowGet);
            }

            // ask questions!!!
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            if (userId == string.Empty || userId == null)
            {
                return this.Json(new { message = "Invalid user!", success = false }, JsonRequestBehavior.AllowGet);
            }

            int isSaved = 0;

            Task responseTask = Task.Run(() =>
            {
                isSaved = this.userService.AddToCart(userId, Guid.Parse(productId));
            });

            responseTask.Wait();

            if (isSaved > 0)
            {
                return this.Json(new { message = "Successfully added to cart.", success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new { message = "Order failed!", success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
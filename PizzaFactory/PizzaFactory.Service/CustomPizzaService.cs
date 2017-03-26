﻿using PizzaFactory.Service.Contracts;
using PizzaFactory.Data;
using PizzaFactory.Service.Models;
using PizzaFactory.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using PizzaFactory.Service.Helpers;

namespace PizzaFactory.Service
{
    public class CustomPizzaService : ICustomPizzaService
    {
        private IMapper mapper;
        private IPizzaFactoryDbContext pizzaContext;
        private IValidator validator;

        public CustomPizzaService(IPizzaFactoryDbContext pizzaContext, IMapper mapper, IValidator validator)
        {
            this.pizzaContext = pizzaContext;
            this.mapper = mapper;
            this.validator = validator;
        }

        public int Create(CreateCustomPizzaModel customPizzaModel)
        {
            var pizza = this.mapper.FromCreateCustomPizzaModel(this.pizzaContext, customPizzaModel);

            this.pizzaContext.CustomPizzas.Add(pizza);

            return this.pizzaContext.SaveChanges();
        }

        // DEPRECATED
        //public IEnumerable<CustomPizzaModel> GetAll()
        //{
        //    var pizzas = this.pizzaContext.CustomPizzas.ToList();
        //    var customPizzaModels = new List<CustomPizzaModel>();

        //    foreach (var item in pizzas)
        //    {
        //        customPizzaModels.Add(new CustomPizzaModel()
        //        {
        //            Name = item.Name,
        //            Price = item.Price,
        //            Ingredients = item.Ingredients.Select(i => new IngredientModel() { Id = i.Id, Name = i.Name, Price = i.Price }).ToList(),
        //            Description = item.Description
        //        });
        //    }

        //    return customPizzaModels;
        //}

        public IEnumerable<CustomPizzaModel> GetAllWithPaging(out int count, int page = 1, int size = 10, Func<CustomPizza, object> sortBy = null)
        {
            page = validator.ValidatePage(page);
            size = validator.ValidatePageSize(size);

            if (sortBy == null)
            {
                sortBy = cp => cp.Name;
            }

            var pizzas = this.pizzaContext.CustomPizzas.OrderBy(sortBy).Skip(size * (page - 1)).Take(size).ToList();
            var customPizzaModels = this.mapper.FromCustomPizzas(pizzas);

            count = this.pizzaContext.CustomPizzas.Count();

            return customPizzaModels;
        }
    }
}

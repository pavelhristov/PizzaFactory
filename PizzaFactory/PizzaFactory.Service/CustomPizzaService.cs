using PizzaFactory.Service.Contracts;
using PizzaFactory.Data;
using PizzaFactory.Service.Models;
using PizzaFactory.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using PizzaFactory.Service.Helpers;
using Bytes2you.Validation;

namespace PizzaFactory.Service
{
    public class CustomPizzaService : ICustomPizzaService
    {
        private IMapper mapper;
        private IPizzaFactoryDbContext pizzaContext;
        private IValidator validator;

        public CustomPizzaService(IPizzaFactoryDbContext pizzaContext, IMapper mapper, IValidator validator)
        {
            Guard.WhenArgument(pizzaContext, nameof(pizzaContext)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(validator, nameof(validator)).IsNull().Throw();

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
        public IEnumerable<CustomPizzaModel> GetAll()
        {
            var pizzas = this.pizzaContext.CustomPizzas.ToList();
            var customPizzaModels = new List<CustomPizzaModel>();

            foreach (var item in pizzas)
            {
                customPizzaModels.Add(new CustomPizzaModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Ingredients = item.Ingredients.Select(i => new IngredientModel() { Id = i.Id, Name = i.Name, Price = i.Price }).ToList(),
                    Description = item.Description
                });
            }

            return customPizzaModels;
        }

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

        public CustomPizzaModel GetById(Guid id)
        {
            var pizza = this.pizzaContext.CustomPizzas.Find(id);
            var ingredients = new List<IngredientModel>();
            foreach (var item in pizza.Ingredients)
            {
                ingredients.Add(new IngredientModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price
                });
            }
            var customPizza = new CustomPizzaModel()
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Description = pizza.Description,
                Ingredients = ingredients,
                Price = pizza.Price
            };
            
            return customPizza;
        }
    }
}

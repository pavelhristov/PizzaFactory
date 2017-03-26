using PizzaFactory.Service.Contracts;
using System;
using System.Collections.Generic;
using PizzaFactory.Data.Models;
using PizzaFactory.Data;
using PizzaFactory.Service.Models;
using System.Linq;

namespace PizzaFactory.Service
{
    public class IngredientService : IIngredientService
    {
        private IPizzaFactoryDbContext pizzaContext;

        public IngredientService(IPizzaFactoryDbContext pizzaContext)
        {
            this.pizzaContext = pizzaContext;
        }

        public IEnumerable<IngredientModel> GetAll()
        {
            return this.pizzaContext.Ingredients.ToList().Select(i => new IngredientModel() { Id = i.Id, Name = i.Name, Price = i.Price });
        }

        public IngredientModel GetById(Guid? Id)
        {
            if (!Id.HasValue)
            {
                return null;
            }

            var ingredient = this.pizzaContext.Ingredients.Find(Id);
            var ingredientModel = new IngredientModel()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Price = ingredient.Price
            };

            return ingredientModel;
        }
    }
}

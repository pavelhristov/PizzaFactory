using PizzaFactory.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaFactory.Data.Models;
using PizzaFactory.Data;

namespace PizzaFactory.Service
{
    public class IngredientService : IIngredientService
    {
        private IPizzaFactoryDbContext pizzaContext;

        public IngredientService(IPizzaFactoryDbContext pizzaContext)
        {
            this.pizzaContext = pizzaContext;
        }

        public IQueryable<Ingredient> GetAll()
        {
            return this.pizzaContext.Ingredients;
        }

        public Ingredient GetById(Guid? Id)
        {
            if (!Id.HasValue)
            {
                return null;
            }

            return this.pizzaContext.Ingredients.Find(Id);
        }
    }
}

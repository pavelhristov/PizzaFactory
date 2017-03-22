using System;
using System.Linq;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Data;
using Bytes2you.Validation;

namespace PizzaFactory.Service
{
    public class PizzaService : IPizzaService
    {
        private IPizzaFactoryDbContext pizzaContext;

        public PizzaService(IPizzaFactoryDbContext pizzaContext)
        {
            Guard.WhenArgument(pizzaContext, nameof(pizzaContext)).IsNull().Throw();

            this.pizzaContext = pizzaContext;
        }

        public IQueryable<Pizza> GetAll()
        {
            return this.pizzaContext.Pizzas;
        }

        public Pizza GetById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            return this.pizzaContext.Pizzas.Find(id);
        }

        public int Create(string name)
        {
            Pizza pizza = new Pizza()
            {
                Name = name
            };

            this.pizzaContext.Pizzas.Add(pizza);
            return this.pizzaContext.SaveChanges();
        }
    }
}

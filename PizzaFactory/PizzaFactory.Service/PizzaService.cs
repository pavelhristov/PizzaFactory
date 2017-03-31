using System;
using System.Linq;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Data;
using Bytes2you.Validation;
using PizzaFactory.Service.Models;
using System.Collections.Generic;

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

        public IEnumerable<PizzaModel> GetAll()
        {
            return this.pizzaContext.Pizzas.ToList().Select(p=> new PizzaModel(p));
        }

        public PizzaModel GetById(Guid id)
        {
            var pizza = this.pizzaContext.Pizzas.Find(id);

            return new PizzaModel(pizza);
        }
    }
}

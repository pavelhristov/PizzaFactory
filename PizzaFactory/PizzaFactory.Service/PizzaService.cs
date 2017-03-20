using System;
using System.Linq;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Data;

namespace PizzaFactory.Service
{
    public class PizzaService : IPizzaService
    {
        private IPizzaFactoryDbContext pizzaContext;

        public PizzaService(IPizzaFactoryDbContext pizzaContext)
        {
            this.pizzaContext = pizzaContext;
        }

        public IQueryable<Pizza> GetAll()
        {
            return this.pizzaContext.Pizzas;
        }
    }
}

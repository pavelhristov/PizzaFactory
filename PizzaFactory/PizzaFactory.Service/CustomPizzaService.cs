using PizzaFactory.Service.Contracts;
using PizzaFactory.Data.Models;
using PizzaFactory.Data;

namespace PizzaFactory.Service
{
    public class CustomPizzaService : ICustomPizzaService
    {
        private IPizzaFactoryDbContext pizzaContext;

        public CustomPizzaService(IPizzaFactoryDbContext pizzaContext)
        {
            this.pizzaContext = pizzaContext;
        }

        public int Create(CustomPizza pizza)
        {
            this.pizzaContext.CustomPizzas.Add(pizza);

            return this.pizzaContext.SaveChanges();
        }
    }
}

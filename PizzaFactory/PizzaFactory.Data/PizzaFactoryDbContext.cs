using System.Data.Entity;
using PizzaFactory.Data.Models;

namespace PizzaFactory.Data
{
    public class PizzaFactoryDbContext : DbContext, IPizzaFactoryDbContext
    {
        public PizzaFactoryDbContext()
            : base("DefaultConnection")
        {

        }

        public IDbSet<Pizza> Pizzas { get; set; }
    }
}

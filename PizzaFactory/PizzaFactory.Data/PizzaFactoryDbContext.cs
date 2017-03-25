using System.Data.Entity;
using PizzaFactory.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PizzaFactory.Data
{
    public class PizzaFactoryDbContext : IdentityDbContext<ApplicationUser>, IPizzaFactoryDbContext, IIdentityDbContext
    {
        public PizzaFactoryDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public IDbSet<CustomPizza> CustomPizzas { get; set; }

        public IDbSet<Ingredient> Ingredients { get; set; }

        public IDbSet<Pizza> Pizzas { get; set; }

        public IDbSet<BasePizza> BasePizzas { get; set; }

        public static PizzaFactoryDbContext Create()
        {
            return new PizzaFactoryDbContext();
        }

        public void CreateDatabaseIfNotExists()
        {
            base.Database.CreateIfNotExists();
        }
    }
}

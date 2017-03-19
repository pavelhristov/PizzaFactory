using System.Data.Entity;
using PizzaFactory.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PizzaFactory.Data
{
    public class PizzaFactoryDbContext : IdentityDbContext<ApplicationUser>, IPizzaFactoryDbContext
    {
        public PizzaFactoryDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public IDbSet<Pizza> Pizzas { get; set; }


        public static PizzaFactoryDbContext Create()
        {
            return new PizzaFactoryDbContext();
        }
    }
}

using System.Data.Entity;
using PizzaFactory.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace PizzaFactory.Data
{
    public class PizzaFactoryDbContext : IdentityDbContext<ApplicationUser>, IPizzaFactoryDbContext
    {
        public PizzaFactoryDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public IDbSet<CustomPizza> CustomPizzas { get; set; }

        public IDbSet<Ingredient> Ingredients { get; set; }

        public IDbSet<Pizza> Pizzas { get; set; }

        public IDbSet<Quantity> Quantities { get; set; }

        public IDbSet<IngredientWithQuantity> IngredientWithQuantity { get; set; }


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

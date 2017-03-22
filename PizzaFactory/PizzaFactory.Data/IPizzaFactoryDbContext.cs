using PizzaFactory.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Data
{
    public interface IPizzaFactoryDbContext: IBaseDbContext
    {
        IDbSet<Pizza> Pizzas { get; }

        IDbSet<CustomPizza> CustomPizzas { get; }

        IDbSet<Ingredient> Ingredients { get; }

        IDbSet<Quantity> Quantities { get; }
    }
}

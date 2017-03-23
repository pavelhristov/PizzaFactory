using PizzaFactory.Data.Models;
using System.Data.Entity;

namespace PizzaFactory.Data
{
    public interface IPizzaFactoryDbContext: IBaseDbContext
    {
        IDbSet<Pizza> Pizzas { get; }

        IDbSet<CustomPizza> CustomPizzas { get; }

        IDbSet<Ingredient> Ingredients { get; }
    }
}

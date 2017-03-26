using PizzaFactory.Data;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Models;
using System.Collections.Generic;

namespace PizzaFactory.Service.Helpers
{
    public interface IMapper
    {
        CustomPizza FromCreateCustomPizzaModel(IPizzaFactoryDbContext pizzaContext, CreateCustomPizzaModel customPizzaModel);
        ICollection<CustomPizzaModel> FromCustomPizzas(ICollection<CustomPizza> pizzas);
    }
}

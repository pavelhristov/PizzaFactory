using PizzaFactory.Data;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Service.Helpers
{
    public class Mapper : IMapper
    {
        public CustomPizza FromCreateCustomPizzaModel(IPizzaFactoryDbContext pizzaContext, CreateCustomPizzaModel customPizzaModel)
        {
            var ingredients = new List<Ingredient>();
            decimal price = 2.00M;

            foreach (var item in customPizzaModel.Ingredients)
            {
                var ingredient = pizzaContext.Ingredients.Find(item);
                if (ingredient.Name != "none" && !ingredients.Contains(ingredient))
                {
                    ingredients.Add(ingredient);
                    price += ingredient.Price;
                }
            }

            CustomPizza pizza = new CustomPizza()
            {
                Name = customPizzaModel.Name,
                Description = customPizzaModel.Description,
                Ingredients = ingredients,
                Price = price
            };

            return pizza;
        }

        public ICollection<CustomPizzaModel> FromCustomPizzas(ICollection<CustomPizza> pizzas)
        {
            var customPizzaModels = new List<CustomPizzaModel>();

            foreach (var item in pizzas)
            {
                customPizzaModels.Add(new CustomPizzaModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Ingredients = item.Ingredients.Select(i => new IngredientModel() { Id = i.Id, Name = i.Name, Price = i.Price }).ToList(),
                    Description = item.Description
                });
            }

            return customPizzaModels;
        }
    }
}

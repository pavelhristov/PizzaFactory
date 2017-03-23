using PizzaFactory.Service.Contracts;
using PizzaFactory.Data;
using PizzaFactory.Service.Models;
using PizzaFactory.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace PizzaFactory.Service
{
    public class CustomPizzaService : ICustomPizzaService
    {
        private IPizzaFactoryDbContext pizzaContext;

        public CustomPizzaService(IPizzaFactoryDbContext pizzaContext)
        {
            this.pizzaContext = pizzaContext;
        }

        public int Create(CreateCustomPizzaModel customPizzaModel)
        {
            var ingredients = new List<Ingredient>();
            decimal price = 2.00M;

            foreach (var item in customPizzaModel.Ingredients)
            {
                var ingredient = this.pizzaContext.Ingredients.Find(item);
                if (ingredient.Name != "none")
                {
                    ingredients.Add(ingredient);
                }

                price += ingredient.Price;
            }

            this.pizzaContext.CustomPizzas.Add(new CustomPizza()
            {
                Name = customPizzaModel.Name,
                Description = customPizzaModel.Description,
                Ingredients = ingredients,
                Price = price
            });

            return this.pizzaContext.SaveChanges();
        }

        public IEnumerable<CustomPizzaModel> GetAll(int take = 5, int page = 0)
        {
            // the very max per page
            if (take > 10)
            {
                take = 10;
            }

            var pizzas = this.pizzaContext.CustomPizzas.OrderBy(x => x.Name).Skip(page).Take(take).ToList();
            var customPizzaModels = new List<CustomPizzaModel>();

            foreach (var item in pizzas)
            {
                customPizzaModels.Add(new CustomPizzaModel()
                {
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

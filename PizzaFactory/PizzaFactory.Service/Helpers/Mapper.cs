using PizzaFactory.Data;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public BasePizza FromPizzaAndCustomPizza(Pizza pizza, CustomPizza customPizza)
        {
            BasePizza basePizza = new BasePizza()
            {
                CustomPizza = customPizza,
                OurPizza = pizza
            };

            return basePizza;
        }

        public IEnumerable<OrderModel> FromOrders(IEnumerable<Order> orders)
        {
            var orderModels = new List<OrderModel>();

            foreach (var item in orders)
            {
                var pizzaList = new List<string>();
                decimal price = 0M;

                foreach (var pizza in item.Pizzas)
                {
                    if (pizza.CustomPizza != null)
                    {
                        pizzaList.Add(pizza.CustomPizza.Name);
                        price += pizza.CustomPizza.Price;
                    }
                    else if (pizza.OurPizza != null)
                    {
                        pizzaList.Add(pizza.OurPizza.Name);
                        price += pizza.OurPizza.Price;
                    }
                }

                var orderModel = new OrderModel()
                {
                    Id = item.Id,
                    Address = item.Address,
                    CreatedOn = item.CreatedOn,
                    Pizzas = pizzaList,
                    Price = price,
                    User = item.Customer.Email
                };

                orderModels.Add(orderModel);
            }

            return orderModels;
        }

        public IEnumerable<BasePizzaModel> FromBasePizzas(IEnumerable<BasePizza> basePizzas)
        {
            List<BasePizzaModel> pizzaList = new List<BasePizzaModel>();

            foreach (var item in basePizzas)
            {
                var pizza = new BasePizzaModel();
                pizza.Id = item.Id;

                if (item.CustomPizza != null)
                {
                    pizza.Name = item.CustomPizza.Name;
                    pizza.Price = item.CustomPizza.Price;
                }
                else if (item.OurPizza != null)
                {
                    pizza.Name = item.OurPizza.Name;
                    pizza.Price = item.OurPizza.Price;
                }

                pizzaList.Add(pizza);
            }

            return pizzaList;
        }
    }
}

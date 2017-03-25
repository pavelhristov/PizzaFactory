﻿using PizzaFactory.Data;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;

namespace PizzaFactory.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private IIdentityDbContext userContext;
        private IPizzaFactoryDbContext pizzaContext;

        public ApplicationUserService(IIdentityDbContext userContext, IPizzaFactoryDbContext pizzaContext)
        {
            this.userContext = userContext;
            this.pizzaContext = pizzaContext;
        }

        public int AddToCart(string userId, Guid productId)
        {
            var user = this.userContext.Users.Find(userId);

            var pizza = this.pizzaContext.Pizzas.Find(productId);
            var customPizza = this.pizzaContext.CustomPizzas.Find(productId);


            var basePizza = new BasePizza()
            {
                CustomPizza = customPizza,
                OurPizza = pizza
            };

            user.Cart.Add(basePizza);

            return this.userContext.SaveChanges();
        }

        public IEnumerable<BasePizzaModel> UserCart(string userId)
        {
            ApplicationUser user = this.userContext.Users.Find(userId);
            List<BasePizzaModel> pizzaList = new List<BasePizzaModel>();

            foreach (var item in user.Cart)
            {
                var pizza = new BasePizzaModel();

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

        public int ConfirmOrder(string userId, string address)
        {
            throw new NotImplementedException();
        }
    }
}

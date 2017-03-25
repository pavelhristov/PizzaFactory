using PizzaFactory.Data;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Contracts;
using System;

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
    }
}

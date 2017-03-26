using PizzaFactory.Data;
using PizzaFactory.Data.Models;
using PizzaFactory.Service.Contracts;
using PizzaFactory.Service.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using Bytes2you.Validation;
using PizzaFactory.Service.Helpers;

namespace PizzaFactory.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private IIdentityDbContext userContext;
        private IPizzaFactoryDbContext pizzaContext;
        private IOrderDbContext orderContext;
        private IValidator validator;
        private IMapper mapper;

        public ApplicationUserService(IIdentityDbContext userContext, IPizzaFactoryDbContext pizzaContext, IOrderDbContext orderContext, IMapper mapper, IValidator validator)
        {
            Guard.WhenArgument(userContext, nameof(userContext)).IsNull().Throw();
            Guard.WhenArgument(pizzaContext, nameof(pizzaContext)).IsNull().Throw();
            Guard.WhenArgument(orderContext, nameof(orderContext)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(validator, nameof(validator)).IsNull().Throw();

            this.userContext = userContext;
            this.pizzaContext = pizzaContext;
            this.orderContext = orderContext;
            this.mapper = mapper;
            this.validator = validator;
        }

        public int AddToCart(string userId, Guid productId)
        {
            var user = this.userContext.Users.Find(userId);

            var pizza = this.pizzaContext.Pizzas.Find(productId);
            var customPizza = this.pizzaContext.CustomPizzas.Find(productId);
            
            var basePizza = this.mapper.FromPizzaAndCustomPizza(pizza, customPizza);

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

        public int ConfirmOrder(string userId, string address)
        {
            ApplicationUser user = this.userContext.Users.Find(userId);
            Order order = new Order()
            {
                Address = address,
                Customer = user,
                Pizzas = user.Cart,
                CreatedOn = DateTime.Now
            };

            this.orderContext.Orders.Add(order);

            user.Cart = new List<BasePizza>();


            return this.orderContext.SaveChanges();
        }

        public int RemoveFromCart(string userId, Guid productId)
        {
            var user = this.userContext.Users.Find(userId);

            var pizza = user.Cart.FirstOrDefault(p => p.Id == productId);

            if (pizza != null)
            {
                user.Cart.Remove(pizza);
            }

            return this.userContext.SaveChanges();
        }

        public IEnumerable<OrderModel> GetAllOrdersWithPaging(out int count, int page = 1, int size = 10, Func<Order, object> sortBy = null)
        {
            if (page < 1)
            {
                page = 1;
            }

            if (size < 1)
            {
                size = 1;
            }

            if (size > 10)
            {
                size = 10;
            }

            if (sortBy == null)
            {
                sortBy = cp => cp.CreatedOn;
            }


            var orders = this.orderContext.Orders.OrderByDescending(sortBy).Skip(size * (page - 1)).Take(size).ToList();
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


            count = orderContext.Orders.Count();
            return orderModels;
        }
    }
}

using System;
using System.Collections.Generic;

namespace PizzaFactory.WebClient.Areas.Administration.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            this.Pizzas = new List<string>();
        }

        public OrderViewModel(Guid id, string userName, ICollection<string> pizzaNames, decimal price, DateTime createdOn, string address)
        {
            this.Id = id;
            this.User = userName;
            this.Pizzas = pizzaNames;
            this.Price = price;
            this.CreatedOn = createdOn;
            this.Address = address;
        }

        public Guid Id { get; set; }

        public string User { get; set; }

        public ICollection<string> Pizzas { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Address { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace PizzaFactory.Service.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }

        public string User { get; set; }

        public ICollection<string> Pizzas { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Address { get; set; }
    }
}

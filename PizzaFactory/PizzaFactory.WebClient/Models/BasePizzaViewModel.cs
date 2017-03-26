using System;

namespace PizzaFactory.WebClient.Models
{
    public class BasePizzaViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
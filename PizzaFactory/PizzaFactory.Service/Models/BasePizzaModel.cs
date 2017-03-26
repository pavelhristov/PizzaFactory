using System;

namespace PizzaFactory.Service.Models
{
    public class BasePizzaModel
    {
        public Guid Id { get; set; } 

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}

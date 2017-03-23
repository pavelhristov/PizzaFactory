using System;

namespace PizzaFactory.Service.Models
{
    public class IngredientModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
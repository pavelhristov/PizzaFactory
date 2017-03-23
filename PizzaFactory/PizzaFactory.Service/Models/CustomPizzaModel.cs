using System;
using System.Collections.Generic;

namespace PizzaFactory.Service.Models
{
    public class CreateCustomPizzaModel
    {
        public CreateCustomPizzaModel()
        {
            this.Ingredients = new HashSet<Guid>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<Guid> Ingredients { get; set; }
    }

    public class CustomPizzaModel
    {
        public CustomPizzaModel()
        {
            this.Ingredients = new HashSet<IngredientModel>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<IngredientModel> Ingredients { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFactory.Data.Models
{
    public class CustomPizza
    {
        private ICollection<Ingredient> ingredients;

        public CustomPizza()
        {
            this.ingredients = new HashSet<Ingredient>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Ingredient> Ingredients
        {
            get
            {
                return this.ingredients;
            }
            set
            {
                this.ingredients = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFactory.Data.Models
{
    public class Ingredient
    {
        private ICollection<CustomPizza> customPizzas;

        public Ingredient()
        {
            this.customPizzas = new HashSet<CustomPizza>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<CustomPizza> CustomPizzas
        {
            get
            {
                return this.customPizzas;
            }
            set
            {
                this.customPizzas = value;
            }
        }
    }
}

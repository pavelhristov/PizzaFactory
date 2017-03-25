using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFactory.Data.Models
{
    public class Pizza
    {
        private ICollection<BasePizza> basePizzas;

        public Pizza()
        {
            this.basePizzas = new List<BasePizza>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }

        public virtual ICollection<BasePizza> BasePizzas
        {
            get
            {
                return basePizzas;
            }

            set
            {
                basePizzas = value;
            }
        }
    }
}

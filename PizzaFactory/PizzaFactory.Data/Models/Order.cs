using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFactory.Data.Models
{
    public class Order
    {
        private ICollection<BasePizza> pizzas;

        public Order()
        {
            this.pizzas = new HashSet<BasePizza>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual ApplicationUser Customer { get; set; }

        public virtual ICollection<BasePizza> Pizzas
        {
            get
            {
                return this.pizzas;
            }
            set
            {
                this.pizzas = value;
            }
        }

        public DateTime CreatedOn { get; set; }

        public string Address { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFactory.Data.Models
{
    public class BasePizza
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual CustomPizza CustomPizza { get; set; }

        public virtual Pizza OurPizza { get; set; } 

        public virtual ApplicationUser User { get; set; }
    }
}

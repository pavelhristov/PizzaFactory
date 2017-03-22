using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Data.Models
{
    public class IngredientWithQuantity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Ingredient Ingredient { get; set; }

        public Quantity Quantity { get; set; }

        public virtual CustomPizza CustomPizza { get; set; }
    }
}

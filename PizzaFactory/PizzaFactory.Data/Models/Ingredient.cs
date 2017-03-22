using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFactory.Data.Models
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}

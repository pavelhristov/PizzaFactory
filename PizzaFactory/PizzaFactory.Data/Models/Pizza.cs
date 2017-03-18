using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaFactory.Data.Models
{
    public class Pizza
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

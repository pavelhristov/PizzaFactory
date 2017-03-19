using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaFactory.WebClient.Models
{
    public class PizzaViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
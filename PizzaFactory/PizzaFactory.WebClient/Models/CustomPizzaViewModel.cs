using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaFactory.WebClient.Models
{
    public class CreateCustomPizzaViewModel
    {
        public CreateCustomPizzaViewModel()
        {
            Ingredients = new List<Guid>();
        }
        
        public string Name { get; set; }

        [DisplayName("Comment about the Order")]
        public string Description { get; set; }
        
        public ICollection<Guid> Ingredients { get; set; }
    }

    public class ListCustomPizzaViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public decimal Price { get; set; }
    }
}
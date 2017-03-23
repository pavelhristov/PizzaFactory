using PizzaFactory.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PizzaFactory.WebClient.Models
{
    public class CustomPizzaViewModel
    {
        public CustomPizzaViewModel()
        {
            Ingredients = new List<Guid>();
        }
        
        public string Name { get; set; }

        [DisplayName("Comment about the Order")]
        public string Description { get; set; }
        
        public ICollection<Guid> Ingredients { get; set; }
    }
}
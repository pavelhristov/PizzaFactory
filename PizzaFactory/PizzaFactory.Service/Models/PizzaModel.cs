using PizzaFactory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Service.Models
{
    public class PizzaModel
    {
        public PizzaModel()
        {

        }

        public PizzaModel(Pizza pizza)
        {
            this.Id = pizza.Id;
            this.Name = pizza.Name;
            this.Description = pizza.Description;
            this.ImgUrl = pizza.ImgUrl;
            this.Price = pizza.Price;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImgUrl { get; set; }
    }
}

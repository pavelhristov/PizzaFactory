using PizzaFactory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Tests.Helpers
{
    public class Helper
    {
        public static IEnumerable<Pizza> GetPizzas()
        {
            return new List<Pizza>()
            {
                new Pizza()
                {
                    Name = "Margarita",
                    ImgUrl = "https://qph.ec.quoracdn.net/main-qimg-311ad5650cf27f9a806ada70a21a2678-c",
                    Description = "It is not a flower!",
                    Price = 4.50M
                },

                new Pizza()
                {
                    Name = "Napoli",
                    ImgUrl = "http://www.napolipizzaastoria.com/images/104827009.jpg.jpg",
                    Description = "Big Pizza!",
                    Price = 5.50M
                },

                new Pizza()
                {
                    Name = "Pizza Cake",
                    ImgUrl = "https://d12xickik43a9a.cloudfront.net/images/magazine/de/M29069-Pizza-Cake-Q85-375.jpg",
                    Description = "Does not exist in real life.",
                    Price = 15.50M
                }
            };
        }

        public static IEnumerable<Ingredient> GetIngredients()
        {
            return new List<Ingredient>()
            {
                new Ingredient()
                {
                    Name = "none",
                    Price = 0.00M
                },
                new Ingredient()
                {
                    Name = "Cheese",
                    Price = 0.80M
                },
                new Ingredient()
                {
                    Name = "Chicken fillet",
                    Price = 1.30M
                },
                new Ingredient()
                {
                    Name = "Tomatoes",
                    Price = 0.50M
                }
            };
        }
    }
}

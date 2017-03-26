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
    }
}

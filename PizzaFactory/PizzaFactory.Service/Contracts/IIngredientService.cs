using PizzaFactory.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaFactory.Service.Contracts
{
    public interface IIngredientService
    {
        IQueryable<Ingredient> GetAll();
    }
}

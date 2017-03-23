using PizzaFactory.Data.Models;
using System;
using System.Linq;

namespace PizzaFactory.Service.Contracts
{
    public interface IIngredientService
    {
        IQueryable<Ingredient> GetAll();

        Ingredient GetById(Guid? Id);
    }
}

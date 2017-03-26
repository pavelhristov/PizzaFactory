using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;

namespace PizzaFactory.Service.Contracts
{
    public interface IIngredientService
    {
        IEnumerable<IngredientModel> GetAll();

        //IngredientModel GetById(Guid Id);
    }
}

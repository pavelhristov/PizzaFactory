using PizzaFactory.Data.Models;
using System;
using System.Linq;

namespace PizzaFactory.Service.Contracts
{
    public interface IPizzaService
    {
        IQueryable<Pizza> GetAll();

        Pizza GetById(Guid? id);

        int Create(string name);
    }
}

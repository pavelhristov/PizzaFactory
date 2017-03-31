using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;

namespace PizzaFactory.Service.Contracts
{
    public interface IPizzaService
    {
        IEnumerable<PizzaModel> GetAll();

        PizzaModel GetById(Guid id);
    }
}

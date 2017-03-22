using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaFactory.Service.Contracts
{
    public interface IPizzaService
    {
        IEnumerable<PizzaModel> GetAll();

        PizzaModel GetById(Guid? id);
    }
}

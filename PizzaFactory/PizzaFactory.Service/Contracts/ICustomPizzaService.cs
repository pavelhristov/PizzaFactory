using PizzaFactory.Data.Models;
using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;

namespace PizzaFactory.Service.Contracts
{
    public interface ICustomPizzaService
    {
        int Create(CreateCustomPizzaModel pizza);

        // DEPRECATED
        IEnumerable<CustomPizzaModel> GetAll();

        IEnumerable<CustomPizzaModel> GetAllWithPaging(out int count, int page = 1, int size = 10, Func<CustomPizza, object> sortBy = null);

        CustomPizzaModel GetById(Guid id);
    }
}

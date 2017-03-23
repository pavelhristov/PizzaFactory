using PizzaFactory.Service.Models;
using System.Collections.Generic;

namespace PizzaFactory.Service.Contracts
{
    public interface ICustomPizzaService
    {
        int Create(CreateCustomPizzaModel pizza);

        IEnumerable<CustomPizzaModel> GetAll(int take = 5, int page = 0);
    }
}

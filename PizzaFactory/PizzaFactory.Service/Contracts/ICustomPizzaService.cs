using PizzaFactory.Data.Models;

namespace PizzaFactory.Service.Contracts
{
    public interface ICustomPizzaService
    {
        int Create(CustomPizza pizza);
    }
}

using PizzaFactory.Data.Models;
using System.Linq;

namespace PizzaFactory.Service.Contracts
{
    public interface IPizzaService
    {
        IQueryable<Pizza> GetAll();
    }
}

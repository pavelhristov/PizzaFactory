using PizzaFactory.Data.Models;
using System.Data.Entity;

namespace PizzaFactory.Data
{
    public interface IOrderDbContext : IBaseDbContext
    {
        IDbSet<Order> Orders { get; }
    }
}

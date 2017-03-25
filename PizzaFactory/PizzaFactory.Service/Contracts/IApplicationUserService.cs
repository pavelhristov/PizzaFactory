using System;

namespace PizzaFactory.Service.Contracts
{
    public interface IApplicationUserService
    {
        int AddToCart(string userId, Guid productId);
    }
}

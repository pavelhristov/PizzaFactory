using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;

namespace PizzaFactory.Service.Contracts
{
    public interface IApplicationUserService
    {
        int AddToCart(string userId, Guid productId);

        IEnumerable<BasePizzaModel> UserCart(string userId);

        int ConfirmOrder(string userId, string address);
    }
}

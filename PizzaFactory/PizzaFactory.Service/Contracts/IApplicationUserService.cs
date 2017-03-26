using PizzaFactory.Data.Models;
using PizzaFactory.Service.Models;
using System;
using System.Collections.Generic;

namespace PizzaFactory.Service.Contracts
{
    public interface IApplicationUserService
    {
        int AddToCart(string userId, Guid productId);

        int RemoveFromCart(string userId, Guid productId);

        IEnumerable<BasePizzaModel> UserCart(string userId);

        int ConfirmOrder(string userId, string address);

        IEnumerable<OrderModel> GetAllOrdersWithPaging(out int count, int page = 1, int size = 10, Func<Order, object> sortBy = null);
    }
}

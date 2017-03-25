using Microsoft.AspNet.Identity;
using PizzaFactory.Data.Models;
using System.Data.Entity;

namespace PizzaFactory.Data
{
    public interface IIdentityDbContext: IBaseDbContext
    {
        IDbSet<ApplicationUser> Users { get;}
    }
}

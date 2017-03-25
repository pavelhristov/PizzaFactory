using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaFactory.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<BasePizza> cart;

        public ApplicationUser()
        {
            this.cart = new List<BasePizza>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<BasePizza> Cart
        {
            get
            {
                return this.cart;
            }
            set
            {
                this.cart = value;
            }
        }
    }
}

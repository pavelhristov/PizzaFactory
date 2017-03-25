namespace PizzaFactory.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzaFactoryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PizzaFactoryDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Pizzas.Any())
            {
                var pizza1 = new Pizza()
                {
                    Name = "Margarita",
                    ImgUrl = "https://qph.ec.quoracdn.net/main-qimg-311ad5650cf27f9a806ada70a21a2678-c",
                    Description = "It is not a flower!",
                    Price = 4.50M
                };

                var pizza2 = new Pizza()
                {
                    Name = "Napoli",
                    ImgUrl = "http://www.napolipizzaastoria.com/images/104827009.jpg.jpg",
                    Description = "Big Pizza!",
                    Price = 5.50M
                };

                var pizza3 = new Pizza()
                {
                    Name = "Pizza Cake",
                    ImgUrl = "https://d12xickik43a9a.cloudfront.net/images/magazine/de/M29069-Pizza-Cake-Q85-375.jpg",
                    Description = "Does not exist in real life.",
                    Price = 15.50M
                };

                context.Pizzas.Add(pizza1);
                context.Pizzas.Add(pizza2);
                context.Pizzas.Add(pizza3);
            }


            if (!context.Ingredients.Any())
            {
                context.Ingredients.Add(new Ingredient()
                {
                    Name = "none",
                    Price = 0.00M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Cheese",
                    Price = 0.80M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Chicken fillet",
                    Price = 1.30M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Tomatoes",
                    Price = 0.50M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Pineapple",
                    Price = 0.60M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Olives",
                    Price = 0.40M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Pickles",
                    Price = 0.80M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Philadelphia cream",
                    Price = 0.90M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Ham",
                    Price = 1.00M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Tuna fish",
                    Price = 1.50M
                });

                context.Ingredients.Add(new Ingredient()
                {
                    Name = "Sausage",
                    Price = 1.10M
                });
            }

            if (!context.Roles.Any())
            {
                // Create admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole() { Name = "Admin" };
                roleManager.Create(role);

                // Create admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser { UserName = "admin@pizza.com", Email = "admin@pizza.com" };
                userManager.Create(user, "12345q");

                // Assign user to admin role
                userManager.AddToRole(user.Id, "Admin");
            }


            context.SaveChanges();
        }
    }
}

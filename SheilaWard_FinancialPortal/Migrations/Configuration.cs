namespace SheilaWard_FinancialPortal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SheilaWard_FinancialPortal.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SheilaWard_FinancialPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SheilaWard_FinancialPortal.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));  // Instantiates a RoleManager
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            #region Seed Roles
            //Roles - Admin, HeadOfHousehold, Member
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "HeadOfHousehold"))
            {
                roleManager.Create(new IdentityRole { Name = "HeadOfHousehold" });
            }

            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }

            if (!context.Roles.Any(r => r.Name == "Lobbyist"))
            {
                roleManager.Create(new IdentityRole { Name = "Lobbyist" });
            }
            #endregion

            #region Seed Users
            // Seed yourself
            if (!context.Users.Any(r => r.UserName == "Sheila.Ward@email.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Sheila.Ward@email.com",
                    Email = "Sheila.Ward@email.com",
                    FirstName = "Sheila",
                    LastName = "Ward",
                    AvatarUrl = "/Avatars/sheila.jpg"
                }, "P@ssw0rd");
            }
            #endregion

            #region Assign Roles to Users
            // Assign yourself role of Admin
            var sheilaId = userManager.FindByEmail("Sheila.Ward@email.com").Id;
            userManager.AddToRole(sheilaId, "Admin");
            #endregion

            #region Seed Transaction Type
            // Seed types of Deposit, Withdrawal, Adjustment, Fee
            context.TransactionTypes.AddOrUpdate(
                t => t.Type,
                new TransactionType { Type = "Deposit"},
                new TransactionType { Type = "Withdrawal"},
                new TransactionType { Type = "Adjustment"},
                new TransactionType { Type = "Fee"}
            );
            #endregion

            #region Seed Account Type
            // Seed Checking, Savings, MoneyMarket
            context.AccountTypes.AddOrUpdate(
                t => t.Type,
                new AccountType { Type = "Checking", Description = "A deposit account for everyday expenses" },
                new AccountType { Type = "Savings", Description = "An interest-bearing deposit account that usually has some limitations on withdrawal frequency" },
                new AccountType { Type = "MoneyMarket", Description = "A special type of Savings Account that may have higher interest rates and also allows checks and debit cards.  Usually a high balance must be maintained." }
            );

            context.SaveChanges();
            #endregion

            #region Seed Household
            // Seed a Demo Household
            DateTime H1Date = new DateTime(2019, 10, 02, 19, 00, 14, 16);
            context.Households.AddOrUpdate(
                new Household
                {
                    Name = "Ward Household",
                    Greeting = "Welcome to the Ward Household!",
                    Created = new DateTime(H1Date.Year, H1Date.Month, H1Date.Day, H1Date.Hour, H1Date.Minute, H1Date.Second, H1Date.Millisecond)
                }
            );
            context.SaveChanges();
            #endregion

            #region HOH User
            if(!context.Users.Any(r => r.UserName == "BrooksWard@mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    HouseholdId = context.Households.AsNoTracking().FirstOrDefault(h => h.Name == "Ward Household").Id,
                    UserName = "BrooksWard@mailinator.com",
                    Email = "BrooksWard@mailinator.com",
                    FirstName = "Brooks",
                    LastName = "Ward",
                    AvatarUrl = "/Avatars/default-user-icon-8.jpg"
                }, "P@ssw0rd");
            }

            var userId = userManager.FindByEmail("BrooksWard@mailinator.com").Id;
            userManager.AddToRole(userId, "HeadOfHousehold");
            #endregion

        }
    }
}

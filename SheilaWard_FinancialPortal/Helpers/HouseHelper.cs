using Microsoft.AspNet.Identity;
using SheilaWard_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SheilaWard_FinancialPortal.Helpers
{
    public class HouseHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static bool IsConfigured()
        {
            var configured = false;

            var houseId = db.Users.Find(HttpContext.Current.User.Identity.GetUserId()).HouseholdId;
            var accounts = db.BankAccounts.AsNoTracking().Where(b => b.HouseholdId == houseId);
            var budgets = db.Budgets.AsNoTracking().Where(b => b.HouseholdId == houseId);
            var budgetItems = budgets.SelectMany(b => b.BudgetItems);

            if (accounts.Count() > 0 && budgets.Count() > 0 && budgetItems.Count() > 0)
            {
                configured = true;
            }

            return configured;

        }
    }
}
using SheilaWard_FinancialPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace SheilaWard_FinancialPortal.ViewModels
{
    public class ConfigureViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public BankAccount BankAccount { get; set; }
        public Budget Budget { get; set; }
        public BudgetItem BudgetItem { get; set; }
        //public SelectList AccountTypeId { get; set; }

        public ConfigureViewModel() 
        {
            BankAccount = new BankAccount();
            Budget = new Budget();
            BudgetItem = new BudgetItem();
            //AccountTypeId = new SelectList(db.AccountTypes, "Id", "Type");
        }   
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SheilaWard_FinancialPortal.Models
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Type { get; set; }  //Checking, Savings, etc...
        public string Description { get; set; }
    }
}
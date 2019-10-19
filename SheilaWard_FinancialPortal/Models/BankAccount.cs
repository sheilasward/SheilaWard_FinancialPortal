using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SheilaWard_FinancialPortal.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public int AccountTypeId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal LowBalanceLevel { get; set; }

        public virtual Household Household { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccount()
        {
            Transactions = new HashSet<Transaction>();
        }
    }
}
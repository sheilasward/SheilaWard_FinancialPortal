using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SheilaWard_FinancialPortal.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public DateTime Created { get; set; }
        public string RecipientEmail { get; set; }
        public Guid Code { get; set; }
        public int TTL { get; set; }  //TimeToLive
        public bool Valid { get; set; }

        public virtual Household Household { get; set; }
    }
}
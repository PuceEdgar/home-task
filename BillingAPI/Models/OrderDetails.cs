using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.Models
{
    public class OrderDetails
    {
        public int OrderNumber { get; set; }
        public int UserId { get; set; }
        public double PayableAmount { get; set; }
        public string PaymentGateWay { get; set; }
        public string Description { get; set; }

    }
}

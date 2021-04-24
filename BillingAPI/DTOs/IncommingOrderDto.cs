using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.DTOs
{
    public class IncommingOrderDto
    {
        public int OrderNumber { get; set; }
        public int UserId { get; set; }
        public double PayableAmount { get; set; }
        public string PaymentGateWay { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.DTOs
{
    public class PaymentDetailsDto
    {
        public int OrderNumber { get; set; }
        public double PayableAmount { get; set; }
    }
}

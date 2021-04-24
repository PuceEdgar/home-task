using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.DTOs
{
    public class CreatedOrderDto
    {
        public string Message { get; set; }
        public ReadOrderDto OrderDetails { get; set; }
    }
}

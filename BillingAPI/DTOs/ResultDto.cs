using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.DTOs
{
    public class ResultDto
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<ReadOrderDto> ListOfOrders { get; set; }
        public ReadOrderDto SingleOrder { get; set; }
    }
}

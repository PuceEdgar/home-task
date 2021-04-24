using BillingAPI.DTOs;
using BillingAPI.Logic;
using Microsoft.AspNetCore.Mvc;

namespace PaymentGatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        // GET: api/PaymentGateway
        [Route("process/seb")]
        [HttpPost]
        public IActionResult ProcessSebPayment([FromBody] PaymentDetailsDto value)
        {
            var isValidDto = PayloadValidator.ValidatePaymentDetailsDto(value);
            
            return isValidDto ? StatusCode(200) : StatusCode(400);
        }

        [Route("process/luminor")]
        [HttpPost]
        public IActionResult ProcessLuminorPayment([FromBody] PaymentDetailsDto value)
        {
            var isValidDto = PayloadValidator.ValidatePaymentDetailsDto(value);

            return isValidDto ? StatusCode(200) : StatusCode(400);
        }

        [Route("process/swedbank")]
        [HttpPost]
        public IActionResult ProcessSwedbankPayment([FromBody] PaymentDetailsDto value)
        {
            var isValidDto = PayloadValidator.ValidatePaymentDetailsDto(value);

            return isValidDto ? StatusCode(200) : StatusCode(400);
        }

    }
}

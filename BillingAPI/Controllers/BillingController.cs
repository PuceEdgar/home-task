using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BillingAPI.Data;
using BillingAPI.DTOs;
using BillingAPI.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IOrderRepo _orders;
        private readonly IHttpClientFactory _clientFactory;

        public BillingController(IOrderRepo orderRepo, IHttpClientFactory clientFactory)
        {
            _orders = orderRepo;
            _clientFactory = clientFactory;
        }

        // GET: api/Billing
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _orders.GetAllOrders();
                if (result.Success)
                {
                    return Ok(result.ListOfOrders);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Billing/userorders/5
        [Route("userorders/{userId:int}")]
        [HttpGet]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            
            try
            {
                var result = await _orders.GetOrdersByUserId(userId);
                if (result.Success)
                {
                    return Ok(result.ListOfOrders);
                }
                else
                {
                    return NotFound(result.Message);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/Billing/order/5
        [Route("order/{orderNumber:int}")]
        [HttpGet]
        public async Task<IActionResult> GetOrder(int orderNumber)
        {
            try
            {
                var result = await _orders.GetOrderByOrderNumber(orderNumber);
                if (result.Success)
                {
                    return Ok(result.SingleOrder);
                }
                else
                {
                    return NotFound(result.Message);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Billing
        [HttpPost]
        public async Task<IActionResult> ProcessOrder(IncommingOrderDto order)
        {
            try
            {
                var validationResult = PayloadValidator.ValidateOrderDetails(order);
                if (!validationResult.Item1)
                {
                    return BadRequest(string.Join(", ", validationResult.Item2));
                }
                
                var paymentResult = await PaymentProcessor.ProcessPayment(_clientFactory , order);
                if (!paymentResult.Item1)
                {
                    return BadRequest(paymentResult.Item2);
                }

                var result = await _orders.AddNewOrderDetails(order);

                if (!result.Success)
                {
                    BadRequest(result.Message);
                }

                var uri = $"{Request.Scheme}://{Request.Host}{Request.Path}/order/{result.SingleOrder.OrderNumber}";
                return Created(uri, DtoMapper.MapResultDtoToCreatedOrderDto(result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using BillingAPI.DTOs;
using BillingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.Logic
{
    public class DtoMapper
    {
        public static OrderDetails MapIncommingOrderDtoToOrderDetailsModel(IncommingOrderDto createOrderDto)
        {
            return new OrderDetails
            {
                OrderNumber = createOrderDto.OrderNumber,
                UserId = createOrderDto.UserId,
                PayableAmount = createOrderDto.PayableAmount,
                PaymentGateWay = createOrderDto.PaymentGateWay,
                Description = createOrderDto.Description
            };
        }

        public static ReadOrderDto MapOrderDetailsModelToReadOrderDto(OrderDetails orderDetails)
        {
            return new ReadOrderDto
            {
                OrderNumber = orderDetails.OrderNumber,
                UserId = orderDetails.UserId,
                PayableAmount = orderDetails.PayableAmount,
                PaymentGateWay = orderDetails.PaymentGateWay,
                Description = orderDetails.Description
            };
        }

        public static PaymentDetailsDto MapIncommingOrderDtoToPaymentDetailsDto(IncommingOrderDto orderDetails)
        {
            return new PaymentDetailsDto
            {
                OrderNumber = orderDetails.OrderNumber,
                PayableAmount = orderDetails.PayableAmount
            };
        }

        public static CreatedOrderDto MapResultDtoToCreatedOrderDto(ResultDto result)
        {
            return new CreatedOrderDto
            {
                Message = result.Message,
                OrderDetails = result.SingleOrder
            };
        }
    }
}

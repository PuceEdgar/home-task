using BillingAPI.DTOs;
using BillingAPI.Logic;
using BillingAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BillingAPI.Tests
{
    public class DtoMapperTests
    {
        [Fact]
        public void MapIncommingOrderDtoToOrderDetailsModel_ShouldReturnOrderDetailsType()
        {
            var incommingOrder = new IncommingOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" };

            var actual = DtoMapper.MapIncommingOrderDtoToOrderDetailsModel(incommingOrder);

            Assert.IsType<OrderDetails>(actual);
        }
        [Fact]
        public void MapOrderDetailsModelToReadOrderDto_ShouldReturnReadOrderDtoType()
        {
            var orderDetails = new OrderDetails { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" };

            var actual = DtoMapper.MapOrderDetailsModelToReadOrderDto(orderDetails);

            Assert.IsType<ReadOrderDto>(actual);
        }
        [Fact]
        public void MapIncommingOrderDtoToPaymentDetailsDto_ShouldReturnPaymentDetailsType()
        {
            var incommingOrder = new IncommingOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" };

            var actual = DtoMapper.MapIncommingOrderDtoToPaymentDetailsDto(incommingOrder);

            Assert.IsType<PaymentDetailsDto>(actual);
        }

        [Fact]
        public void MapResultDtoToCreatedOrderDto_ShouldReturnCreatedOrderDtoType()
        {
            var result = new ResultDto
            {
                SingleOrder = new ReadOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" },
                Message = "OK"
            };
            var actual = DtoMapper.MapResultDtoToCreatedOrderDto(result);

            Assert.IsType<CreatedOrderDto>(actual);
        }

        [Fact]
        public void MapResultDtoToCreatedOrderDto_ShouldReturnCreatedOrderDto()
        {
            var resultDto = new ResultDto
            {
                SingleOrder = new ReadOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" },
                Message = "OK"
            };

            var createdOrderDto = new CreatedOrderDto { Message = resultDto.Message, OrderDetails = resultDto.SingleOrder };

            var expected = JsonConvert.SerializeObject(createdOrderDto);

            var result = DtoMapper.MapResultDtoToCreatedOrderDto(resultDto);
            var actual = JsonConvert.SerializeObject(result);

            Assert.Equal(expected, actual);
        }
    }
}

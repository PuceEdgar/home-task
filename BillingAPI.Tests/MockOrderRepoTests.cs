using BillingAPI.Constants;
using BillingAPI.Data;
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
    public class MockOrderRepoTests
    {
        [Fact]
        public void GetListOfAllOrders_ShoudReturnAllOrders()
        {
            var mockOrderRepo = new MockOrderRepo();
            var result = mockOrderRepo.GetAllOrders().Result;

            int expected = 4;

            int actual = result.ListOfOrders.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetListOfAllOrders_ShoudReturnNoOrderFound()
        {
            var mockOrderRepo = new MockOrderRepo();
            mockOrderRepo._orders = new List<OrderDetails>();
            
            var expected = Messages.NoOrdersFound();

            var result = mockOrderRepo.GetAllOrders().Result;
            var actual = result.Message;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetOrderByOrderNumber_ShoudReturnOrder()
        {
            var mockOrderRepo = new MockOrderRepo();

            var readOrderDto = new ReadOrderDto { OrderNumber = 2, UserId = 456, PayableAmount = 31.12, PaymentGateWay = "Luminor", Description = "This is order number 2" };
            var expected = JsonConvert.SerializeObject(readOrderDto);

            var result = mockOrderRepo.GetOrderByOrderNumber(2).Result;
            var actual = JsonConvert.SerializeObject(result.SingleOrder);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetOrderByOrderNumber_ShoudReturnOrderNotFound()
        {
            var mockOrderRepo = new MockOrderRepo();
            var orderNumber = 23;
            var expected = Messages.OrderNotFound(orderNumber);

            var result = mockOrderRepo.GetOrderByOrderNumber(orderNumber).Result;
            var actual = result.Message;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(123, 2)]
        [InlineData(456, 1)]
        [InlineData(45678, 0)]
        public void GetOrdersByUserId_ShoudReturnAllUserOrders(int userId, int expected)
        {
            var mockOrderRepo = new MockOrderRepo();

            var result = mockOrderRepo.GetOrdersByUserId(userId).Result;
            var actual = result.ListOfOrders.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetOrdersByUserId_ShoudReturnUserOrdersNotFound()
        {
            var mockOrderRepo = new MockOrderRepo();
            var userId = 12345;

            var expected = Messages.UserOrdersNotFound(userId);

            var result = mockOrderRepo.GetOrdersByUserId(userId).Result;
            var actual = result.Message;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddNewOrderDetails_ShoudReturnAddedOrder()
        {
            var mockOrderRepo = new MockOrderRepo();
            var incommingOrder = new IncommingOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" };

            var expected = JsonConvert.SerializeObject(incommingOrder);

            var result = mockOrderRepo.AddNewOrderDetails(incommingOrder).Result;
            var actual = JsonConvert.SerializeObject(result.SingleOrder);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddNewOrderDetails_ShoudReturnCreatedMessage()
        {
            var mockOrderRepo = new MockOrderRepo();
            var incommingOrder = new IncommingOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" };
            
            var expected = Messages.OrderCreatedSuccessfully(incommingOrder.OrderNumber);

            var result = mockOrderRepo.AddNewOrderDetails(incommingOrder).Result;
            var actual = result.Message;

            Assert.Equal(expected, actual);
        }
    }
}

using BillingAPI.Controllers;
using BillingAPI.Data;
using BillingAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Web.Http.Results;
using Xunit;

namespace BillingAPI.Tests
{
    public class BillingControllerTests
    {
        [Fact]
        public void Get_ShouldReturnStatusCode200()
        {
            var orderList = new MockOrderRepo();
            var orders = new Mock<IOrderRepo>();
            orders.Setup(x => x.GetAllOrders()).Returns(orderList.GetAllOrders());
            var clientFactory = new Mock<IHttpClientFactory>();

            var billingController = new BillingController(orders.Object, clientFactory.Object);
            var result = billingController.Get().Result;
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<List<ReadOrderDto>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetUserOrders_ShouldReturnStatusCode200()
        {
            var userId = 123;
            var orderList = new MockOrderRepo();
            var orders = new Mock<IOrderRepo>();
            orders.Setup(x => x.GetOrdersByUserId(userId)).Returns(orderList.GetOrdersByUserId(userId));
            var clientFactory = new Mock<IHttpClientFactory>();

            var billingController = new BillingController(orders.Object, clientFactory.Object);
            var result = billingController.GetUserOrders(userId).Result;
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<List<ReadOrderDto>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void GetOrder_ShouldReturnStatusCode200()
        {
            var orderNumber = 2;
            var orderList = new MockOrderRepo();
            var orders = new Mock<IOrderRepo>();
            orders.Setup(x => x.GetOrderByOrderNumber(orderNumber)).Returns(orderList.GetOrderByOrderNumber(orderNumber));
            var clientFactory = new Mock<IHttpClientFactory>();

            var billingController = new BillingController(orders.Object, clientFactory.Object);
            var result = billingController.GetOrder(orderNumber).Result;
            var okResult = result as OkObjectResult;

            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<ReadOrderDto>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public void ProcessOrder_ShouldReturnStatusCode201()
        {
            var incommingOrder = new IncommingOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "Seb", Description = "This is order number 5" };
            var orderList = new MockOrderRepo();
            var orders = new Mock<IOrderRepo>();
            orders.Setup(x => x.AddNewOrderDetails(incommingOrder)).Returns(orderList.AddNewOrderDetails(incommingOrder));
            
            var clientFactory = new Mock<IHttpClientFactory>();
            clientFactory.Setup(x => x.CreateClient(incommingOrder.PaymentGateWay)).Returns(() => 
            { 
                var client = new HttpClient(); 
                client.BaseAddress = new Uri("https://localhost:6001/api/paymentgateway/process/seb");
                return client; }
            );

            var billingController = new BillingController(orders.Object, clientFactory.Object);
            var result = billingController.ProcessOrder(incommingOrder).Result;
            var okResult = result as ObjectResult;

            Assert.NotNull(okResult);
            Assert.True(okResult is ObjectResult);
            Assert.IsType<CreatedOrderDto>(okResult.Value);
            Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);
        }
    }
}

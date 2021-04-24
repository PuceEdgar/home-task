using BillingAPI.Controllers;
using BillingAPI.Data;
using BillingAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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

    }
}

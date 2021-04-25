using BillingAPI.Constants;
using BillingAPI.DTOs;
using BillingAPI.Logic;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BillingAPI.Tests
{
    public class PaymentProcessorTests
    {
        [Fact]        
        public void ProcessPayment_ShouldReturnError()
        {
            var paymentGateway = "abc";
            var incommingOrder = new IncommingOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = paymentGateway, Description = "This is order number 5" };
            var clientFactory = new Mock<IHttpClientFactory>();

            var expected = Messages.PaymentGatewayNotFound();

            var actual = PaymentProcessor.ProcessPayment(clientFactory.Object, incommingOrder).Result;

            Assert.False(actual.Item1);
            Assert.Equal(expected, actual.Item2);
        }

        [Fact]
        public void Process_ShouldProcessPayment()
        {
            var incommingOrder = new IncommingOrderDto { OrderNumber = 5, UserId = 456, PayableAmount = 99.12, PaymentGateWay = "seb", Description = "This is order number 5" };
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:6001/api/paymentgateway/process/seb")
            };

            var actual = PaymentProcessor.Process(client, incommingOrder).Result;

            Assert.True(actual.Item1);
        }
    }
}

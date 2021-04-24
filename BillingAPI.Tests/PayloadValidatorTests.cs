using BillingAPI.Constants;
using BillingAPI.DTOs;
using BillingAPI.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BillingAPI.Tests
{
    public class PayloadValidatorTests
    {
        [Fact]
        public void ValidateInteger_ShouldReturnErrorMessage()
        {
            var integer = 0;
            var valueType = "user";
            List<string> errors = new List<string>();

            var expected = Messages.IntegerErrorMessage(valueType);

            PayloadValidator.ValidateInteger(integer, valueType, errors);

            var actual = errors.FirstOrDefault();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidatePayableAmount_ShouldReturnErrorMessage()
        {
            var amount = -22;
            List<string> errors = new List<string>();

            var expected = Messages.PayableAmountErrorMessage();

            PayloadValidator.ValidatePayableAmount(amount, errors);

            var actual = errors.FirstOrDefault();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidatePayementGateway_ShouldReturnErrorMessage()
        {
            var gateway = "";
            List<string> errors = new List<string>();

            var expected = Messages.PaymentGatewayNotSelected();

            PayloadValidator.ValidatePayementGateway(gateway, errors);

            var actual = errors.FirstOrDefault();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 12, true)]
        [InlineData(5, -12.33, false)]
        public void ValidatePaymentDetailsDto_ShouldReturnCorrectBool(int orderNumber, double amount, bool expected)
        {
            var paymentDetails = new PaymentDetailsDto { OrderNumber = orderNumber, PayableAmount = amount };

            var actual = PayloadValidator.ValidatePaymentDetailsDto(paymentDetails);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 12, 12, "seb", "success", true)]
        [InlineData(5, 333, -12.33, "sebs", "fail", false)]
        [InlineData(5, 12, 35, "luminor", "success", true)]
        [InlineData(5, 12, -35, "luminor", "fail", false)]
        public void ValidateOrderDetails_ShouldReturnCorrectBool(int orderNumber, int userId, double amount, string gateway, string description, bool expected)
        {
            var incommingOrder = new IncommingOrderDto { OrderNumber = orderNumber, UserId = userId, PayableAmount = amount, PaymentGateWay = gateway, Description = description };

            var actual = PayloadValidator.ValidateOrderDetails(incommingOrder).Item1;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 12, 12, "seb", "success", 0)]
        [InlineData(5, 333, -12.33, "sebs", "fail", 1)]
        [InlineData(5, 12, -35, "", "fail", 2)]
        [InlineData(0, 12, -35, "   ", "fail", 3)]
        public void ValidateOrderDetails_ShouldReturnCorrectErrorCount(int orderNumber, int userId, double amount, string gateway, string description, int expected)
        {
            var incommingOrder = new IncommingOrderDto { OrderNumber = orderNumber, UserId = userId, PayableAmount = amount, PaymentGateWay = gateway, Description = description };

            var actual = PayloadValidator.ValidateOrderDetails(incommingOrder).Item2.Count;

            Assert.Equal(expected, actual);
        }
    }
}

using BillingAPI.Constants;
using BillingAPI.DTOs;
using BillingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAPI.Logic
{
    public class PayloadValidator
    {
        public static (bool, List<string>) ValidateOrderDetails(IncommingOrderDto payload)
        {
            var isValid = true;
            var errors = new List<string>();
            var error = string.Empty;

            ValidateInteger(payload.OrderNumber, "Order Number", errors);
            ValidateInteger(payload.UserId, "User Id", errors);
            ValidatePayableAmount(payload.PayableAmount, errors);
            ValidatePayementGateway(payload.PaymentGateWay, errors);

            if (errors.Count() > 0)
            {
                isValid = false;
                //error = string.Join(", ", errors);
            }
            return (isValid, errors);
        }

        public static bool ValidatePaymentDetailsDto(PaymentDetailsDto payload)
        {
            var isValid = true;
            var errors = new List<string>();

            ValidateInteger(payload.OrderNumber, "Order Number", errors);
            ValidatePayableAmount(payload.PayableAmount, errors);

            if (errors.Count() > 0)
            {
                isValid = false;
            }
            return isValid;
        }

        public static void ValidatePayementGateway(string payementGateway, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(payementGateway))
            {
                errors.Add(Messages.PaymentGatewayNotSelected());
            }
        }

        public static void ValidatePayableAmount(double amount, List<string> errors)
        {
            if (amount <= 0)
            {
                errors.Add(Messages.PayableAmountErrorMessage());
            }
        }

        public static void ValidateInteger(int value, string valueType, List<string> errors)
        {
            if (value == 0)
            {
                errors.Add(Messages.IntegerErrorMessage(valueType));
            }
        }
    }
}

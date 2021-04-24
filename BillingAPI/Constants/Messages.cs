namespace BillingAPI.Constants
{
    public class Messages
    {
        public static string PaymentGatewayNotSelected() => "Payment gateway is not selected!";
        public static string PaymentGatewayNotFound() => "Payement gateway not found!";
        public static string PayableAmountErrorMessage() => "Amount cannot be 0 or less!";
        public static string IntegerErrorMessage(string value) => $"{value} cannot be empty!";
        public static string OrderNotFound(int orderNumber) => $"Order with number: {orderNumber} does not exist!";
        public static string UserOrdersNotFound(int userId) => $"User with id: {userId} doesn't have any orders yet!";
        public static string FailedToCreateOrder(int orderNumber) => $"Failed to create order with number: {orderNumber}";
        public static string NoOrdersFound() => "No orders found!";
        public static string OrderCreatedSuccessfully(int orderNumber) => $"Order number: {orderNumber} was created!";
    }
}

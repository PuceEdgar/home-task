using BillingAPI.Constants;
using BillingAPI.DTOs;
using BillingAPI.Logic;
using BillingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.Data
{
    public class MockOrderRepo : IOrderRepo
    {
        public List<OrderDetails> _orders;

        public MockOrderRepo()
        {
            _orders = new List<OrderDetails>() {
            new OrderDetails { OrderNumber = 1, UserId = 123, PayableAmount = 25.72, PaymentGateWay = "SEB", Description = "This is order number 1" },
            new OrderDetails { OrderNumber = 2, UserId = 456, PayableAmount = 31.12, PaymentGateWay = "Luminor", Description = "This is order number 2" },
            new OrderDetails { OrderNumber = 3, UserId = 789, PayableAmount = 75.27, PaymentGateWay = "Swedbank", Description = "This is order number 3" },
            new OrderDetails { OrderNumber = 4, UserId = 123, PayableAmount = 15.27, PaymentGateWay = "Swedbank", Description = "This is order number 4" }
            };
        }

        private async Task<List<OrderDetails>> GetListOfAllOrders() => await Task.Run(() => _orders);      
        private async Task<OrderDetails> GetOrderByNumber(int number) => await Task.Run(() => _orders.Where(o => o.OrderNumber == number).FirstOrDefault());     
        private async Task<List<OrderDetails>> GetUserOrders(int userId) => await Task.Run(() => _orders.Where(o => o.UserId == userId).ToList());       
        private async Task AddNewOrder(OrderDetails newOrder) => await Task.Run(() => _orders.Add(newOrder));
      
        public async Task<ResultDto> AddNewOrderDetails(IncommingOrderDto orderDetails)
        {
            var result = new ResultDto();
            try
            {
                var newOrder = DtoMapper.MapIncommingOrderDtoToOrderDetailsModel(orderDetails);
                Task.WaitAll(AddNewOrder(newOrder));
                var createdOrder = await GetOrderByNumber(orderDetails.OrderNumber);
                if (createdOrder == null)
                {
                    result.Success = false;
                    result.Message = Messages.FailedToCreateOrder(orderDetails.OrderNumber);
                    return result;
                }

                result.SingleOrder = DtoMapper.MapOrderDetailsModelToReadOrderDto(createdOrder);
                result.Message = Messages.OrderCreatedSuccessfully(createdOrder.OrderNumber);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultDto> GetAllOrders()
        {
            var result = new ResultDto
            {
                ListOfOrders = new List<ReadOrderDto>()
            };
            var allOrders = await GetListOfAllOrders();
            try
            {
                if (allOrders.Count() == 0)
                {
                    result.Success = false;
                    result.Message = Messages.NoOrdersFound();
                    return result;
                }
                
                allOrders.ForEach(o => result.ListOfOrders.Add(DtoMapper.MapOrderDetailsModelToReadOrderDto(o)));
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultDto> GetOrderByOrderNumber(int orderNumber)
        {
            var result = new ResultDto();

            try
            {
                var order = await GetOrderByNumber(orderNumber);
                if (order == null)
                {
                    result.Success = false;
                    result.Message = Messages.OrderNotFound(orderNumber);
                    return result;
                }

                result.SingleOrder = DtoMapper.MapOrderDetailsModelToReadOrderDto(order);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<ResultDto> GetOrdersByUserId(int userId)
        {
            var result = new ResultDto
            {
                ListOfOrders = new List<ReadOrderDto>()
            };
            try
            {
                var userOrders = await GetUserOrders(userId);
                if (userOrders.Count() == 0)
                {
                    result.Success = false;
                    result.Message = Messages.UserOrdersNotFound(userId);
                    return result;
                }
                
                userOrders.ForEach(o => result.ListOfOrders.Add(DtoMapper.MapOrderDetailsModelToReadOrderDto(o)));
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }
    }
}

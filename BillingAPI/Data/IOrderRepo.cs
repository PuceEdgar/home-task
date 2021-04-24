using BillingAPI.DTOs;
using BillingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.Data
{
    public interface IOrderRepo
    {
        Task<ResultDto> GetAllOrders();
        Task<ResultDto> GetOrderByOrderNumber(int orderNumber);
        Task<ResultDto> GetOrdersByUserId(int userId);
        Task<ResultDto> AddNewOrderDetails(IncommingOrderDto orderDetails);
    }
}

using BillingAPI.Enums;
using BillingAPI.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BillingAPI.Constants;

namespace BillingAPI.Logic
{
    public class PaymentProcessor
    {
        public async static Task<(bool, string)> ProcessPayment(IHttpClientFactory clientFactory, IncommingOrderDto createOrderDto)
        {
            if (Enum.TryParse<PaymentGateways>(createOrderDto.PaymentGateWay, true, out var result))
            {
                return await Process(clientFactory.CreateClient(result.ToString()), createOrderDto);
            }
            else
            {
                return (false, Messages.PaymentGatewayNotFound());
            }
        }

        public static async Task<(bool, string)> Process(HttpClient client, IncommingOrderDto createOrderDto)
        {
            try
            {
                var paymentDto = DtoMapper.MapIncommingOrderDtoToPaymentDetailsDto(createOrderDto);
                var json = JsonConvert.SerializeObject(paymentDto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                string result = "";
                var response = await client.PostAsync(client.BaseAddress, data);
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    result = response.ReasonPhrase;
                }

                return (response.IsSuccessStatusCode, result);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}

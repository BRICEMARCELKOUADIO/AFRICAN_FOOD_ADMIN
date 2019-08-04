using AFRICAN_FOOD.Constants;
using AFRICAN_FOOD.Contracts.Repository;
using AFRICAN_FOOD.Contracts.Services.Data;
using AFRICAN_FOOD.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Services.Data
{
    public class OrderDataService : IOrderDataService
    {
        private readonly IGenericRepository _genericRepository;

        public OrderDataService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<Order> PlaceOrder(Order order)
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.PlaceOrderEndpoint
            };

            var result =
                await _genericRepository.PostAsync<Order>(builder.ToString(), order);

            return order;
        }
    }
}

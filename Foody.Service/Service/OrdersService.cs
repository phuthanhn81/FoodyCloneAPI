using Foody.Data.Entities;
using Foody.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foody.Service.Service
{
    public interface IOrdersService
    {
        Task<int> RequestOrders(Orders order, List<OrderDetails> orderDetail);
    }

    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;

        public OrdersService(IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository)
        {
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
        }

        public async Task<int> RequestOrders(Orders order, List<OrderDetails> orderDetail)
        {
            int orderID = await _ordersRepository.Create(order);
            return await _orderDetailsRepository.Create(orderDetail, orderID);
        }
    }
}

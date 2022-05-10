using Foody.Data.Entities;
using Foody.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foody.Service.Service
{
    public interface IOrdersService
    {
        Task<int> RequestOrders(Orders order, List<OrderDetails> orderDetail, string username);
    }

    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IUsersRepository _usersRepository;

        public OrdersService(IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailsRepository, IUsersRepository usersRepository)
        {
            _ordersRepository = ordersRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _usersRepository = usersRepository;
        }

        public async Task<int> RequestOrders(Orders order, List<OrderDetails> orderDetail, string username)
        {
            Users user = await _usersRepository.FindByUserName(username);
            order.Account = user.ID;

            int orderID = await _ordersRepository.Create(order);
            return await _orderDetailsRepository.Create(orderDetail, orderID);
        }
    }
}

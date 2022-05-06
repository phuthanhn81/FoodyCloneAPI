using Foody.Data.EF;
using Foody.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Foody.Data.Repositories
{
    public interface IOrdersRepository
    {
        Task<int> Create(Orders request);
    }

    public class OrdersRepository : IOrdersRepository
    {
        private readonly FoodyContext _context;

        public OrdersRepository(FoodyContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Orders request)
        {
            Orders order = new Orders()
            {
                Account = request.Account,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
             await _context.SaveChangesAsync();

            return order.ID;
        }
    }
}

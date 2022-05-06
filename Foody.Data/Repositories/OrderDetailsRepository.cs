using Foody.Data.EF;
using Foody.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foody.Data.Repositories
{
    public interface IOrderDetailsRepository
    {
        Task<int> Create(List<OrderDetails> request, int orderID);
    }

    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly FoodyContext _context;

        public OrderDetailsRepository(FoodyContext context)
        {
            _context = context;
        }

        public async Task<int> Create(List<OrderDetails> request, int orderID)
        {
            try
            {
                foreach (OrderDetails orderDetail in request)
                {
                    orderDetail.OrderID = orderID;
                }

                _context.OrderDetails.AddRange(request);
                await _context.SaveChangesAsync();

                return 200;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }
    }
}

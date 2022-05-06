using Foody.Data.Entities;
using System.Collections.Generic;

namespace Foody.API.Models
{
    public class RequestOrdersModel
    {
        public Orders order { get; set; }
        public List<OrderDetails> orderDetail { get; set; }
    }
}

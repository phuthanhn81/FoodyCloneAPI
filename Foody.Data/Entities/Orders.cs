using System;

namespace Foody.Data.Entities
{
    public class Orders
    {
        public int ID { get; set; }
        public int Account { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

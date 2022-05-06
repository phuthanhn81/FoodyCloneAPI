using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Data.Entities
{
    public class PlaceDishes
    {
        public int ID { get; set; }
        public int PlaceID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PhotoDish { get; set; }
    }
}

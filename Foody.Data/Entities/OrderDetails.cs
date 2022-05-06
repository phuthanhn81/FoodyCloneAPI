namespace Foody.Data.Entities
{
    public class OrderDetails
    {
        public int OrderID { get; set; }
        public int DishID { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}

namespace Foody.Data.Entities
{
    public partial class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public int? Rating { get; set; }
        public int? City { get; set; }
        public int? Type { get; set; }
    }
}

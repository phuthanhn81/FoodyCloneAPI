namespace Foody.Data.Entities
{
    public partial class Places
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public int? Rating { get; set; }
        public int? City { get; set; }
        public int? Review { get; set; }
        public int? Type { get; set; }
        public string Phone { get; set; }
    }
}

namespace TheCarHub.Areas.Admin.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime YearDate { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}

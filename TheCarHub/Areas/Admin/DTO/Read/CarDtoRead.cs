namespace TheCarHub.Areas.Admin.DTO.Read
{
    public class CarDtoRead
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string VIN { get; set; }
        public string Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public DateTime LotDate { get; set; }
        public decimal SellingPrice { get; set; }
    }
}

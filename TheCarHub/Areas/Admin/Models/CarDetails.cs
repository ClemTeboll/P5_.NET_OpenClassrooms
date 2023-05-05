namespace TheCarHub.Areas.Admin.Models
{
    public class CarDetails
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Purchase { get; set; }
        public string Repairs { get; set; }
        public decimal RepairsCost { get; set;}
        public DateTime LotDate { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime SaleDate { get; set; }


        public int CarId { get; set; } // required foreign key property
        public Car Car { get; set; } // required reference navigation to principal
    }
}

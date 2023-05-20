using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCarHub.Areas.Admin.DTO
{
    public class CarDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string VIN { get; set; }
        [Required]
        public string Year { get; set; }
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Trim { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public decimal Purchase { get; set; }
        public string Repairs { get; set; }
        public decimal RepairsCost { get; set; }
        [Required]
        public DateTime LotDate { get; set; }
        public decimal SellingPrice { get; set; }
        public DateTime SaleDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheCarHub.Areas.Admin.Models;

namespace TheCarHub.Areas.Admin.DTO.Write
{
    public class CarDtoWriteEdit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string? UrlImage { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

        [Required]
        public string VIN { get; set; }
        [Required]
        public int Year { get; set; }
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
        public DateTime? SaleDate { get; set; }
        [Required]
        public int CarMakesId { get; set; }
        [Required]
        public int CarModelId { get; set; }
        
    }
}

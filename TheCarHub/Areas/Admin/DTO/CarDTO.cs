using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCarHub.Areas.Admin.DTO
{
    public class CarDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime YearDate { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        //public string UrlImage { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Image { get; set; }
    }
}

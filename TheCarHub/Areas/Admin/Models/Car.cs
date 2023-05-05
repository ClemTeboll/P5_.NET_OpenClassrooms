using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCarHub.Areas.Admin.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string UrlImage { get; set; }
        public string Description { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Image { get; set; }
        public DateTime YearDate { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }


        public virtual ICollection<CarImage> Images { get; set; }
        public CarDetails CarDetails { get; set; }
    }
}

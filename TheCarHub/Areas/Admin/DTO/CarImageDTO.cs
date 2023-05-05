using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCarHub.Areas.Admin.DTO
{
    public class CarImageDTO
    {
        public string UrlImage { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Image { get; set; }
    }
}

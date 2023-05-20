using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCarHub.Areas.Admin.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        public string UrlImage { get; set; }
        //[NotMapped]
        //[Required]
        //public IFormFile Image { get; set; }

        
        public int CarId { get; set; } // required foreign key property
        public virtual Car Car { get; set; } // required reference navigation to principal
    }
}

namespace TheCarHub.Areas.Admin.Models
{
    public class CarImage
    {
        public int Id { get; set; }
        public string UrlImage { get; set; }
        public IFormFile Image { get; set; }


        public int CarId { get; set; } // required foreign key property
        public Car Car { get; set; } = null!; // required reference navigation to principal
    }
}

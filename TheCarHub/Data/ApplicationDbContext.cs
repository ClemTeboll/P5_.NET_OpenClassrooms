using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Areas.Admin.Models;
using TheCarHub.Areas.Admin.DTO.Read;

namespace TheCarHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarDetails> CarDetails { get; set; }

        public DbSet<CarImage>CarImages { get; set; }

        public DbSet<TheCarHub.Areas.Admin.Models.CarModel> CarModel { get; set; } = default!;

        public DbSet<TheCarHub.Areas.Admin.Models.CarMakes> CarMakes { get; set; } = default!;

        public DbSet<TheCarHub.Areas.Admin.DTO.Read.CarDtoRead> CarDtoRead { get; set; } = default!;
    }
}
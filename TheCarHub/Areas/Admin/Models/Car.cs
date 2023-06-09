﻿namespace TheCarHub.Areas.Admin.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<CarImage> CarImages { get; set; }
        public CarDetails CarDetails { get; set; }
    }
}

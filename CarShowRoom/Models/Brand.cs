using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
    public class Brand : SharedProp
    {
        public int BrandId { get; set; }

        [Required]
        public string BrandName { get; set; }

        public string? BrandImg { get; set; }

        public string BrandText { get; set; }

        // Navigation property to link to car models
        public ICollection<CarModel> CarModels { get; set; }

        // Navigation property to link to news related to the brand
        public ICollection<News> News { get; set; }

        // Constructor to initialize the collections
        public Brand()
        {
            CarModels = new List<CarModel>();
            News = new List<News>();
        }
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Models
{
	public class CarModel:SharedProp
	{
		public int CarModelId { get; set; }
		public string CarModelName { get; set; }
		public string ? CarImage { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
       
       
        public string ? VideoUrl { get; set; }

		[ForeignKey(nameof(Brand))]
		public int BrandId { get; set; }

		public Brand? Brand { get; set; }

        public ICollection<CarImage>? CarImages { get; set; }


    }

}

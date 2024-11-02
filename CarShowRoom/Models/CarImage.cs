using System.ComponentModel.DataAnnotations.Schema;

namespace CarShowRoom.Models
{
	public class CarImage
	{
		public int CarImageId { get; set; }
		public string ImageUrl { get; set; }

		[ForeignKey(nameof(CarModel))]
		public int CarModelId { get; set; }

		public CarModel CarModel { get; set; }

	}
}

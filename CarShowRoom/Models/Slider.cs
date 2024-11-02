using System.ComponentModel.DataAnnotations;

namespace CarShowRoom.Models
{
	public class Slider:SharedProp
	{
		public int SliderId { get; set; }
		[Required]
		public string SliderImg { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; }
	}
}

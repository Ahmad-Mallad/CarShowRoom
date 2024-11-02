namespace CarShowRoom.Models.ViewModels
{
    public class CarModelViewModel
    {
        public int CarModelId { get; set; }  
        public string CarModelName { get; set; }
        public string CarImage { get; set; }
        public string ?VideoUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }


    public class BrandDetailsModel
	{
		public string BrandName { get; set; }
		public string BrandImg { get; set; }
		public List<CarModelViewModel> CarModels { get; set; }
	}
}

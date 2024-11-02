namespace CarShowRoom.Models
{
	public class News : SharedProp
	{
		public int NewsId { get; set; }

        public string ? NewsImg { get; set; }
        public string NewsTitle { get; set; }
		public string NewsContent { get; set; }
		public DateTime PublishDate { get; set; }
		public int BrandId { get; set; }

		// Navigation property to link news to a brand
		public Brand ? Brand { get; set; }
	}

}

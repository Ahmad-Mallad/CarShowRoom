namespace CarShowRoom.Models
{
    public class ContactUs:SharedProp
    {
        public int ContactUsId { get; set; }
        public string ContactUsTitle { get; set; }


        // Basic contact information
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        // Additional fields for the embedded map location
        public string  MapUrl { get; set; }  // Store the URL of the Google Maps embed

        // Optional message or description
        public string ? Description { get; set; }  // For "Get in Touch" or other custom text
    }
}

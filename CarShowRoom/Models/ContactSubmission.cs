namespace CarShowRoom.Models
{
    public class ContactSubmission
    {
        public int ContactSubmissionId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsViewed { get; set; }
        public bool IsRead { get; set; } // Add this property if it doesn't exist
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}

namespace CarShowRoom.Models
{
    public class Feature:SharedProp
    {
        public int FeatureId { get; set; }                   // Unique identifier for each feature
        public string? IconClass { get; set; }          // Stores the icon class for each feature, e.g., "fa fa-car"
        public string? Title { get; set; }              // The title of the feature, e.g., "Wide Range of Models"
        public string? Description { get; set; }       // A brief description of the feature
    }
}

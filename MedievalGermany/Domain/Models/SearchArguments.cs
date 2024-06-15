namespace MedievalGermany.Domain.Models
{
    public class SearchArguments
    {
        public string? Suchtext { get; set; }
        public BoundingBox? BoundingBox { get; set; }
    }

    public record BoundingBox(Location SouthWest, Location NorthEast);

    public record struct Location(double Latitude, double Longitude);
}

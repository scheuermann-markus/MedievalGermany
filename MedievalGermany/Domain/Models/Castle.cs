namespace MedievalGermany.Domain.Models
{
    public class Castle
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public int Eroeffnet { get; set; }
        public string? WikipediaUrl { get; set; }
        public string? ImageString { get; set; }
        public Geolocation? Geolocation { get; set; }
    }

    public class Geolocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}

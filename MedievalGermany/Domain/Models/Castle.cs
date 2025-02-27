using System.ComponentModel.DataAnnotations;

namespace MedievalGermany.Domain.Models;

public class Castle
{
    [Key] public string Id { get; init; } = "Castle/" + Guid.NewGuid();
    [Required] public string Name { get; set; }
    public int? Eroeffnet { get; set; }
    public string? WikipediaUrl { get; set; }
    public string? ImageUrl { get; set; }
    [Required] public Geolocation? Geolocation { get; set; }
}

public class Geolocation
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
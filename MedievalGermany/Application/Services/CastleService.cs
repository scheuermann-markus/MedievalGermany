using MedievalGermany.Application.Interfaces;
using MedievalGermany.Domain.Models;

namespace MedievalGermany.Application.Services
{
    public class CastleService : ICastleService
    {
        public async Task<IEnumerable<Castle>> GetAllCastles()
        {
            await Task.Delay(1000);
            return Data;
        }


        public static IEnumerable<Castle> Data = new List<Castle>
        {
            new Castle
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Schloss Neuschwanstein",
                Eroeffnet = 1884,
                Geolocation = new Geolocation
                {
                    Latitude = 47.557732,
                    Longitude = 10.749646,
                }
            },
            new Castle
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Schloss Heidelberg",
                Eroeffnet = 1214,
                Geolocation = new Geolocation
                {
                    Latitude = 49.41062,
                    Longitude = 8.715309,
                }
            },
        };
    }
}

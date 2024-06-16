using MedievalGermany.Application.Interfaces;
using MedievalGermany.Domain.Models;
using Raven.Client.Documents;

namespace MedievalGermany.Application.Services
{
    public class CastleService : ICastleService
    {
        private readonly IDocumentStore _store;

        public CastleService(IDocumentStore store)
        {
            _store = store;
        }

        public async Task SafeCastle()
        {
            var data = Data.ToList();
            var caslte = data[0];

            using (var session = _store.OpenAsyncSession())
            {
                await session.StoreAsync(caslte);
                await session.SaveChangesAsync();

            } ;

        }


        /// <summary>
        /// Gibt alle Castles zurück, die den SearchArguments entsprechen.
        /// </summary>
        /// <returns>IEnumerable<Castle></returns>
        public async Task<IEnumerable<Castle>> GetCastles(SearchArguments searchArguments)
        {
            var data = Data;

            // Filter nach Suchtext
            if (searchArguments.Suchtext != null)
            {
                data = Data.Where(e => e.Name.Contains(searchArguments.Suchtext, StringComparison.InvariantCultureIgnoreCase));
            }

            // Filter nach BoundingBox
            if (searchArguments.BoundingBox != null)
            {
                var boundingBox = searchArguments.BoundingBox;
                data = data.Where(e => e.Geolocation.Latitude > boundingBox.SouthWest.Latitude 
                                    && e.Geolocation.Latitude < boundingBox.NorthEast.Latitude
                                    && e.Geolocation.Longitude < boundingBox.NorthEast.Longitude
                                    && e.Geolocation.Longitude > boundingBox.SouthWest.Longitude);
            }

            return data;
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
            new Castle
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Schloss Herrenchiemsee",
                Eroeffnet = 1878,
                Geolocation = new Geolocation
                {
                    Latitude = 47.867265,
                    Longitude = 12.39588,
                }
            },
            new Castle
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Schloss Schwerin",
                Eroeffnet = 1857,
                Geolocation = new Geolocation
                {
                    Latitude = 53.624468,
                    Longitude = 11.418173,
                }
            },
        };
    }
}

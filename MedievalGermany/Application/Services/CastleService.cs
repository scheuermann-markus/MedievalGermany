using MediatR;
using MedievalGermany.Application.Interfaces;
using MedievalGermany.Application.Queries;
using MedievalGermany.Domain.Models;
using Raven.Client.Documents;

namespace MedievalGermany.Application.Services
{
    public class CastleService : ICastleService
    {
        private readonly IDocumentStore _store;
        private IMediator _mediator;

        public CastleService(IDocumentStore store, IMediator mediator)
        {
            _store = store;
            _mediator = mediator;
        }

        public async Task SafeCastle()
        {
            //var data = Data.ToList();
            //var caslte = data[2];

            //using (var session = _store.OpenAsyncSession())
            //{
            //    await session.StoreAsync(caslte);
            //    await session.SaveChangesAsync();

            //};

            throw new NotImplementedException();
        }


        /// <summary>
        /// Gibt alle Castles zurück, die den SearchArguments entsprechen.
        /// </summary>
        /// <returns>IEnumerable<Castle></returns>
        public async Task<IEnumerable<Castle>> GetCastles(SearchArguments searchArguments, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetCastles.Query() { SearchArguments = searchArguments }, cancellationToken);

            return result;
        }


        //public static IEnumerable<Castle> Data = new List<Castle>
        //{
        //    new Castle
        //    {
        //        Id = "Castle/" + Guid.NewGuid().ToString(),
        //        Name = "Schloss Neuschwanstein",
        //        Eroeffnet = 1884,
        //        Geolocation = new Geolocation
        //        {
        //            Latitude = 47.557732,
        //            Longitude = 10.749646,
        //        }
        //    },
        //    new Castle
        //    {
        //        Id = "Castle/" + Guid.NewGuid().ToString(),
        //        Name = "Schloss Heidelberg",
        //        Eroeffnet = 1214,
        //        Geolocation = new Geolocation
        //        {
        //            Latitude = 49.41062,
        //            Longitude = 8.715309,
        //        }
        //    },
        //    new Castle
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = "Schloss Herrenchiemsee",
        //        Eroeffnet = 1878,
        //        Geolocation = new Geolocation
        //        {
        //            Latitude = 47.867265,
        //            Longitude = 12.39588,
        //        }
        //    },
        //    new Castle
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Name = "Schloss Schwerin",
        //        Eroeffnet = 1857,
        //        Geolocation = new Geolocation
        //        {
        //            Latitude = 53.624468,
        //            Longitude = 11.418173,
        //        }
        //    },
        //};
    }
}

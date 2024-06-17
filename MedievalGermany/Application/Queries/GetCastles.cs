using MediatR;
using MedievalGermany.Domain.Models;
using Raven.Client.Documents;

namespace MedievalGermany.Application.Queries;

public static class GetCastles
{
    public class Query : IRequest<IEnumerable<Castle>>
    {
        public SearchArguments SearchArguments { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Castle>>
    {
        private readonly IDocumentStore _store;
        public Handler(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<IEnumerable<Castle>> Handle(Query request, CancellationToken cancellationToken)
        {
            using var session = _store.OpenAsyncSession();

            IQueryable<Castle> query = session.Query<Castle>();

            // Filter nach Suchtext
            if (!string.IsNullOrEmpty(request.SearchArguments.Suchtext))
            {
                query = query.Where(e => e.Name.Contains(request.SearchArguments.Suchtext));
            }
            // Filter nach MapView
            if (request.SearchArguments.BoundingBox != null)
            {
                var boundingBox = request.SearchArguments.BoundingBox;
                query = query.Where(e => e.Geolocation.Latitude > boundingBox.SouthWest.Latitude
                                    && e.Geolocation.Latitude < boundingBox.NorthEast.Latitude
                                    && e.Geolocation.Longitude < boundingBox.NorthEast.Longitude
                                    && e.Geolocation.Longitude > boundingBox.SouthWest.Longitude);
            }

            var castles = await query.ToListAsync(cancellationToken);

            return castles;
        }
    }
}

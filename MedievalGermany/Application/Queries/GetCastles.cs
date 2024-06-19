using MediatR;
using MedievalGermany.Application.Indexes;
using MedievalGermany.Domain.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System.Text.RegularExpressions;

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

            IAsyncDocumentQuery<Castle> query = session.Advanced.AsyncDocumentQuery<Castle>();

            // Filter nach Suchtext 
            // Todo: performantere Lösung finden, vll mit einem Index
            if (!string.IsNullOrEmpty(request.SearchArguments.Suchtext))
            {
                var pattern = Regex.Escape(request.SearchArguments.Suchtext);
                query = query.WhereRegex(e => e.Name, pattern);
            }

            // Filter nach MapView
            if (request.SearchArguments.BoundingBox != null)
            {
                var boundingBox = request.SearchArguments.BoundingBox;
                query = query
                    .WhereGreaterThan(e => e.Geolocation.Latitude, boundingBox.SouthWest.Latitude)
                    .WhereLessThan(e => e.Geolocation.Latitude, boundingBox.NorthEast.Latitude)
                    .WhereGreaterThan(e => e.Geolocation.Longitude, boundingBox.SouthWest.Longitude)
                    .WhereLessThan(e => e.Geolocation.Longitude, boundingBox.NorthEast.Longitude);
            }

          

            var castles = await query.ToListAsync(cancellationToken);

            return castles;
        }
    }
}

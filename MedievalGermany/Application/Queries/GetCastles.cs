using MediatR;
using MedievalGermany.Domain.Models;
using MedievalGermany.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MedievalGermany.Application.Queries;

public static class GetCastles
{
    public class Query : IRequest<IEnumerable<Castle>>
    {
        public SearchArguments SearchArguments { get; set; }
    }

    public class Handler : IRequestHandler<Query, IEnumerable<Castle>>
    {
        private readonly ApplicationDbContext _dbContext;
        
        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Castle>> Handle(Query request, CancellationToken cancellationToken)
        {
            IQueryable<Castle> query = _dbContext.Castles;

            // Filter nach Suchtext
            if (!string.IsNullOrEmpty(request.SearchArguments.Suchtext))
            {
                string searchText = request.SearchArguments.Suchtext;
                query = query.Where(e => EF.Functions.ILike(e.Name, $"%{searchText}%"));
            }

            // Filter nach MapView
            if (request.SearchArguments.BoundingBox != null)
            {
                var boundingBox = request.SearchArguments.BoundingBox;
                query = query
                    .Where(e => e.Geolocation.Latitude > boundingBox.SouthWest.Latitude &&
                                e.Geolocation.Latitude < boundingBox.NorthEast.Latitude &&
                                e.Geolocation.Longitude > boundingBox.SouthWest.Longitude &&
                                e.Geolocation.Longitude < boundingBox.NorthEast.Longitude);
            }

            var castles = await query.ToListAsync(cancellationToken);
            
            return castles;
        }
    }
}

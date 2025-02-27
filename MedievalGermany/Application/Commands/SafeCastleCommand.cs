using MediatR;
using MedievalGermany.Domain.Models;
using MedievalGermany.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MedievalGermany.Application.Commands;

public class SafeCastleCommand
{
    public class Command : IRequest
    {
        public Castle Castle { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ApplicationDbContext _dbContext;
        
        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var existingCastle = await _dbContext.Castles
                .FirstOrDefaultAsync(c => c.Id == request.Castle.Id, cancellationToken);
                
            if (existingCastle == null)
            {
                // Add new castle
                await _dbContext.Castles.AddAsync(request.Castle, cancellationToken);
            }
            else
            {
                // Update existing castle
                _dbContext.Entry(existingCastle).CurrentValues.SetValues(request.Castle);
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

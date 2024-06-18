using MediatR;
using MedievalGermany.Domain.Models;
using Raven.Client.Documents;

namespace MedievalGermany.Application.Commands;

public class SafeCastleCommand
{
    public class Command : IRequest
    {
        public Castle Castle { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private IDocumentStore _store;
        public Handler(IDocumentStore store)
        {
            _store = store;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            using var session = _store.OpenAsyncSession();
            await session.StoreAsync(request.Castle, cancellationToken);
            await session.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

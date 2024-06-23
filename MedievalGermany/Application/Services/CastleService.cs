using MediatR;
using MedievalGermany.Application.Commands;
using MedievalGermany.Application.Interfaces;
using MedievalGermany.Application.Queries;
using MedievalGermany.Domain.Models;
using Raven.Client.Documents;
using static Raven.Client.Constants;

namespace MedievalGermany.Application.Services
{
    public class CastleService : ICastleService
    {
        private readonly IDocumentStore _store;
        private IMediator _mediator;
        private readonly IConfiguration _configuration;

        public CastleService(IDocumentStore store, IMediator mediator, IConfiguration configuration)
        {
            _store = store;
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task SafeCastle(Castle castle, string uploadKey)
        {
            if (uploadKey == _configuration.GetValue<string>("Upload:UploadKey"))
            {
                await _mediator.Send(new SafeCastleCommand.Command() { Castle = castle });   
            }
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
    }
}

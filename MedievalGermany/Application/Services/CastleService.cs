using MediatR;
using MedievalGermany.Application.Commands;
using MedievalGermany.Application.Interfaces;
using MedievalGermany.Application.Queries;
using MedievalGermany.Domain.Models;

namespace MedievalGermany.Application.Services
{
    public class CastleService : ICastleService
    {
        private IMediator _mediator;

        public CastleService(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Speichert eine Castle in die Datenbank
        /// </summary>
        /// <param name="castle"></param>
        /// <param name="key"></param>
        public async Task SafeCastle(Castle castle, string key)
        {
            //var uploadKey = Environment.GetEnvironmentVariable("UPLOAD_KEY");
            var uploadKey = "dummyKey";

            if (key == uploadKey)
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

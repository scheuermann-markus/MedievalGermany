using MedievalGermany.Domain.Models;

namespace MedievalGermany.Application.Interfaces
{
    public interface ICastleService
    {
        Task<IEnumerable<Castle>> GetCastles(SearchArguments searchArguments);
    }
}
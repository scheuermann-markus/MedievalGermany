using Raven.Client.Documents;

namespace MedievalGermany.Application.Interfaces
{
    public interface IRavenDbService
    {
        IDocumentStore GetDocumentStore();
    }
}
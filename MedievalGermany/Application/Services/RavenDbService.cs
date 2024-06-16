using MedievalGermany.Application.Interfaces;
using Raven.Client.Documents;
using System.Security.Cryptography.X509Certificates;

namespace MedievalGermany.Application.Services
{
    public class RavenDbService : IRavenDbService
    {
        readonly Lazy<IDocumentStore> _store;
        readonly IConfiguration _configuration;
        public RavenDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            _store = new Lazy<IDocumentStore>(CreateDocumentStore());
        }

        public IDocumentStore GetDocumentStore()
        {
            return _store.Value;
        }

        private IDocumentStore CreateDocumentStore()
        {
            IDocumentStore documentStore;

            var serverUrl = _configuration.GetSection("RavenDbSettings:Urls").Get<string[]>();
            var databaseName = _configuration.GetValue<string>("RavenDbSettings:DatabaseName");
            var certFile = _configuration.GetValue<string>("RavenDbSettings:CertFilePath");

            var clientCertificate = new X509Certificate2(certFile);
            documentStore = new DocumentStore
            {
                Certificate = clientCertificate,
                Urls = serverUrl,
                Database = databaseName,
            };

            documentStore.Initialize();

            return documentStore;
        }
    }
}

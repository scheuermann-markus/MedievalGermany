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
            var certThumbprint = _configuration.GetValue<string>("RavenDbSettings:CertThumbprint");

            X509Certificate2 clientCertificate = null;
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, certThumbprint, false);
                if (certCollection.Count > 0)
                {
                    clientCertificate = certCollection[0];
                }
                store.Close();
            }

            if (clientCertificate != null)
            {
                documentStore = new DocumentStore
                {
                    Certificate = clientCertificate,
                    Urls = serverUrl,
                    Database = databaseName,
                };

                documentStore.Initialize();

                return documentStore;
            }
            else
            {
                throw new Exception("Was unable to load Certificate in 'RavenDbService'");
            }
            
        }
    }
}

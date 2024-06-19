using MedievalGermany.Domain.Models;
using Raven.Client.Documents.Indexes;
using System;

namespace MedievalGermany.Application.Indexes
{
    public class Castles_ByName : AbstractIndexCreationTask<Castle>
    {
        public Castles_ByName()
        {
            Map = castles => from castle in castles
                             select new 
                             {
                                 castle.Name
                             };

            Indexes.Add(x => x.Name, FieldIndexing.Search);
        }
    }
}
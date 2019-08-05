using System;
using System.Threading.Tasks;
using Laklp.Platform.Data.Entities;

namespace Laklp.Platform.Data.Repositories.Interfaces
{
    public interface IDocumentaryRepository
    {
        Task<DocumentaryResource> FetchResourceAsync(Guid id);

        Task<DocumentaryResource>
            SaveOrUpdateAsync(DocumentaryResource documentaryResource);
    }
}
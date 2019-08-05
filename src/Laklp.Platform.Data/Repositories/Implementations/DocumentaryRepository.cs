using System;
using System.Threading.Tasks;
using Laklp.Platform.Data.Context;
using Laklp.Platform.Data.Entities;
using Laklp.Platform.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Laklp.Platform.Data.Repositories.Implementations
{
    public class DocumentaryRepository : IDocumentaryRepository
    {
        private readonly LaklpDbContext _laklpDbContext;

        public DocumentaryRepository(LaklpDbContext laklpDbContext)
        {
            _laklpDbContext = laklpDbContext;
        }

        public async Task<DocumentaryResource> FetchResourceAsync(Guid id)
        {
            return await _laklpDbContext.DocumentaryResources.FindAsync(id);
        }

        public async Task<DocumentaryResource> SaveOrUpdateAsync
        (
            DocumentaryResource documentaryResource)
        {
            EntityEntry<DocumentaryResource> entry;
            if (documentaryResource.Id == Guid.Empty)
            {
                entry = _laklpDbContext.DocumentaryResources.Add(documentaryResource);
                entry.Entity.S3Key = $"{entry.Entity.Id}{entry.Entity.Extension}";
            }
            else
            {
                entry = _laklpDbContext.DocumentaryResources.Update(documentaryResource);
            }

            await _laklpDbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
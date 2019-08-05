using System;
using System.Threading.Tasks;
using Laklp.Models.Dtos;
using Laklp.Platform.Data.Entities;

namespace Laklp.Services.Resources
{
    public interface IResourcesService
    {
        Task<DocumentaryResourceDto>
            RegisterResourceAndGetAsyncId(string extension, User author, bool enabled = true);


        Task<DocumentaryResourceDto> FetchResourceAsync(Guid id);
        Task<string> GetResourceUrl(Guid id);
    }
}
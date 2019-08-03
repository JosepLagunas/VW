using System;
using System.Threading.Tasks;
using Laklp.Services.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Laklp.Controllers
{
    [Route("[controller]")]
    public class ResourcesController : Controller
    {
        private readonly IResourcesService _resourcesService;

        public ResourcesController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> FetchResource(Guid id)
        {
            var resource = await _resourcesService.FetchResourceAsync(id);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterResource([FromQuery] string extension,
            [FromQuery] bool enabled = true)
        {
            var registerId = await _resourcesService
                .RegisterResourceAndGetAsyncId(extension, default, enabled);
            return Ok(registerId);
        }

        [HttpGet]
        [Route("url/{id}")]
        public async Task<string> GetResourceUrl(Guid id)
        {
            return await _resourcesService.GetResourceUrl(id);
        }
    }
}
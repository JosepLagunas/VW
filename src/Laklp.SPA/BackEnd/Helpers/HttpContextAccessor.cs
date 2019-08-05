using Microsoft.AspNetCore.Http;

namespace Laklp.Helpers
{
    internal class HttpContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public  HttpContext GetHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }
    }
}
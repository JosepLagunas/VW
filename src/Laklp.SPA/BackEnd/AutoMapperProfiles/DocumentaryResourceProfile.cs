using AutoMapper;
using Laklp.Models;
using Laklp.Models.Dtos;
using Laklp.Platform.Data.Entities;

namespace Laklp.AutoMapperProfiles
{
    public class DocumentaryResourceProfile : Profile
    {
        public DocumentaryResourceProfile()
        {
            CreateMap<DocumentaryResource, DocumentaryResourceDto>();
        }
    }
}
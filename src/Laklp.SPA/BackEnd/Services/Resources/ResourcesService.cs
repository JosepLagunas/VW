using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Laklp.Models.Dtos;
using Laklp.Models.Settings;
using Laklp.Platform.Data.Entities;
using Laklp.Platform.Data.Repositories.Interfaces;
using Laklp.Services.S3Service;

namespace Laklp.Services.Resources
{
    internal class ResourcesService : IResourcesService
    {
        private readonly IDocumentaryRepository _documentaryRepository;
        private readonly DocumentarySettings _documentarySettings;
        private readonly IS3Service _s3Service;
        private readonly IMapper _mapper;

        public ResourcesService(IDocumentaryRepository documentaryRepository,
            IS3Service s3Service, DocumentarySettings documentarySettings, IMapper mapper)
        {
            _documentaryRepository = documentaryRepository;
            _s3Service = s3Service;
            _documentarySettings = documentarySettings;
            _mapper = mapper;
        }

        public async Task<DocumentaryResourceDto> FetchResourceAsync(Guid id)
        {
            var documentResource = await _documentaryRepository.FetchResourceAsync(id);
            return BuildDocumentaryResourceDto(documentResource);
        }

        public async Task<string> GetResourceUrl(Guid id)
        {
            var resource = await _documentaryRepository.FetchResourceAsync(id);

            if (resource == default)
                throw new KeyNotFoundException("resource not found");

            var key = $"{resource.S3Path ?? ""}/{resource.S3Key}";
            key = key.StartsWith('/') ? key.Remove(0, 1) : key;
            return _s3Service.GetS3FileUrl(resource.S3Bucket, key);
        }

        public async Task<DocumentaryResourceDto> RegisterResourceAndGetAsyncId(string extension,
            User author, bool enabled = true)
        {
            var documentaryResource = BuildNewResourceInstance(extension, author, enabled);

            documentaryResource =
                await _documentaryRepository.SaveOrUpdateAsync(documentaryResource);

            return BuildDocumentaryResourceDto(documentaryResource);
        }

        private DocumentaryResource BuildNewResourceInstance(string extension, User author,
            bool enabled)
        {
            var instance = new DocumentaryResource
            {
                S3Bucket = _documentarySettings.S3Bucket,
                S3Path = _documentarySettings.S3Path,
                S3Region = _documentarySettings.S3Region,
                Extension = extension.Replace(".", "")
                    .Insert(0, "."),
                IsEnabled = enabled,
                CreatedBy = author
            };
            instance.SetCreationTimeStamp();
            return instance;
        }

        private DocumentaryResourceDto
            BuildDocumentaryResourceDto(DocumentaryResource documentResource)
        {
            var dto = _mapper.Map<DocumentaryResource, DocumentaryResourceDto>(documentResource);

            var bucket = documentResource.S3Bucket;
            var key = BuildS3Key(documentResource);
            dto.S3Url = _s3Service.GetS3FileUrl(documentResource.S3Bucket, key);
            return dto;
        }

        private static string BuildS3Key(DocumentaryResource documentResource)
        {
            var path = documentResource.S3Path;
            if (path.StartsWith('/'))
            {
                path = path.Remove(0, 1);
            }

            if (path.EndsWith('/'))
            {
                path = path.Remove(path.Length - 1, 1);
            }

            return $"{path}/{documentResource.S3Key}";
        }
    }
}
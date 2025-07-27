using LighthouseSocial.Application.Common;
using LighthouseSocial.Application.Dtos;
using LighthouseSocial.Domain.Interfaces;
using LighthouseSocial.Domain.ValueObjects;

namespace LighthouseSocial.Application.Features.Photo;

public class UploadPhotoHandler
{
    private readonly IPhotoRepository _repository;
    private readonly IPhotoStorageService _storageService;

    public UploadPhotoHandler(IPhotoRepository repository, IPhotoStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    public async Task<Result<Guid>> HandleAsync(PhotoDto dto, Stream content)
    {
        if (content == null || content.Length == 0)
            return Result<Guid>.Fail("Photo content cannot be empty.");

        try
        {
            var savedPhoto = await _storageService.SaveAsync(content, dto.FileName);
            var metaData = new PhotoMetadata("N/A", "Unknown", dto.CameraModel, dto.UploadedAt);
            var photo = new Domain.Entities.Photo(dto.UserId, dto.LighthouseId, savedPhoto, metaData);

            await _repository.AddAsync(photo);
            return Result<Guid>.Ok(photo.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Fail($"Failed to upload photo: {ex.Message}");
        }
    }
}
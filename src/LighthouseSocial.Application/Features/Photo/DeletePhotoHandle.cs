using LighthouseSocial.Application.Common;
using LighthouseSocial.Domain.Interfaces;

namespace LighthouseSocial.Application.Features.Photo;

public class DeletePhotoHandle
{
    private readonly IPhotoRepository _repository;
    private readonly IPhotoStorageService _storageService;

    public DeletePhotoHandle(IPhotoRepository repository, IPhotoStorageService storageService)
    {
        _repository = repository;
        _storageService = storageService;
    }

    public async Task<Result> HandleAsync(Guid photoId)
    {
        var photo = await _repository.GetByIdAsync(photoId);
        if (photo == null)
            return Result.Fail("Photo not found.");
        try
        {
            await _storageService.DeleteAsync(photo.Filename);
            await _repository.DeleteAsync(photoId);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to delete photo: {ex.Message}");
        }
    }
}
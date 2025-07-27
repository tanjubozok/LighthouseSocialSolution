using LighthouseSocial.Application.Common;
using LighthouseSocial.Application.Dtos;
using LighthouseSocial.Domain.Interfaces;

namespace LighthouseSocial.Application.Features.Comment;

public class GetCommentsByPhotoHandler
{
    private readonly ICommentRepository _repository;

    public GetCommentsByPhotoHandler(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<CommentDto>>> Handle(Guid photoId)
    {
        if (photoId == Guid.Empty)
            return Result<IEnumerable<CommentDto>>.Fail("Invalid photo ID.");
        try
        {
            var comments = await _repository.GetByPhotoIdAsync(photoId);
            if (comments == null || !comments.Any())
                return Result<IEnumerable<CommentDto>>.Ok([]);

            var dtos = comments.Select(c => new CommentDto(c.UserId, c.PhotoId, c.Text, c.Rating.Value));

            return Result<IEnumerable<CommentDto>>.Ok(dtos);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<CommentDto>>.Fail($"An error occurred while retrieving comments: {ex.Message}");
        }
    }
}
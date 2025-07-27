using LighthouseSocial.Application.Common;
using LighthouseSocial.Domain.Interfaces;

namespace LighthouseSocial.Application.Features.Comment;

public class DeleteCommandHandler
{
    private readonly ICommentRepository _repository;
    public DeleteCommandHandler(ICommentRepository repository)
    {
        _repository = repository;
    }
    public async Task<Result> Handle(Guid commentId)
    {
        if (commentId == Guid.Empty)
            return Result.Fail("Invalid comment ID.");
        try
        {
            var comment = await _repository.GetByIdAsync(commentId);
            if (comment == null)
                return Result.Fail("Comment not found.");

            await _repository.DeleteAsync(commentId);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"An error occurred while deleting the comment: {ex.Message}");
        }
    }
}
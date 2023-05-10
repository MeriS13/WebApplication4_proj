using Board.Application.AppData.Contexts.Comments.Repositories;
using Board.Contracts.Comments;
using Board.Domain.Comments;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Board.Application.AppData.Contexts.Comments.Services;



/// <inheritdoc cref="ICommentsService"/>
public class CommentsService : ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentsService(ICommentsRepository commentsRepository, IHttpContextAccessor httpContextAccessor)
    {
        _commentsRepository = commentsRepository;
        _httpContextAccessor = httpContextAccessor;
    }


    // Метод для получения идентификатора текущего авторизованного пользователя из контекста
    public Guid GetCurrentUserId()
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return Guid.Parse(claimId);
    }
    // Метод для получения имени текущего авторизованного пользователя из контекста
    public string GetCurrentUserName()
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        return claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }


    ///<inheritdoc/>
    public async Task<Guid> CreateByPostIdAsync(CreateCommentDto dto, CancellationToken cancellationToken)
    {
        // Преобр из дто в доменную модель
        var entity = new Comment
        {
            UserName = GetCurrentUserName(),
            Content = dto.Content,
            CreationDate = DateTime.UtcNow,
            AccId = GetCurrentUserId(),
            PostId = dto.PostId,
        };

        return await _commentsRepository.CreateByPostIdAsync (entity, cancellationToken);
    }


    ///<inheritdoc/>
    public async Task DeleteByCommentId(Guid id, CancellationToken cancellationToken)
    {
        await _commentsRepository.DeleteByCommentId(id, cancellationToken);
    }


    ///<inheritdoc/>
    public async Task<List<CommentDto>> GetCommentsCurrentUserByIdAsync(CancellationToken cancellationToken)
    {

        List<Comment> entities = _commentsRepository
            .GetCommentsCurrentUserByIdAsync(GetCurrentUserId(), cancellationToken).ToList();
        List<CommentDto> result = new();

        foreach (var entity in entities)
        {
            result.Add(new CommentDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                CreationDate = entity.CreationDate,
                Content = entity.Content,
                PostId = entity.PostId,
                AccId = entity.AccId,
            });
        }
        return result;
    }


    ///<inheritdoc/>
    public async Task<List<CommentDto>> GetCommentsOnPostByIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        List<Comment> entities = _commentsRepository.GetCommentsOnPostByIdAsync(postId, cancellationToken).ToList();
        List<CommentDto> result = new();

        foreach (var entity in entities)
        {
            result.Add(new CommentDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                CreationDate = entity.CreationDate,
                Content = entity.Content,
                PostId = entity.PostId,
                AccId = entity.AccId,
            });
        }
        return result;
    }
}

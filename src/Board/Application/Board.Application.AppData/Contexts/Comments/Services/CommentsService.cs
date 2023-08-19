
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
    public async Task<Guid> CreateCommentAsync(CreateCommentDto dto, CancellationToken cancellationToken)
    {
        // Преобр из дто в доменную модель
        var entity = new Comment
        {
            UserName = GetCurrentUserName(),
            Content = dto.Content,
            CreationDate = DateTime.UtcNow,
            AccId = GetCurrentUserId(),
            PostId = dto.PostId,
            ParComId = Guid.Parse("00000000-0000-0000-0000-000000000000")
        };

        return await _commentsRepository.CreateCommentAsync (entity, cancellationToken);
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
                ParComId = entity.ParComId,
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
                ParComId = entity.ParComId,
            });
        }
        return result;
    }

    public async Task<CommentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _commentsRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var result = new CommentDto
        {
            AccId = entity.AccId,
            UserName = entity.UserName,
            Content = entity.Content,
            PostId = entity.PostId,
            CreationDate = entity.CreationDate,
            Id = entity.Id,
            ParComId = entity.ParComId
        };
        return result;
    }


    public async Task<Guid> CreateAnswer(CreateAnswerDto dto, CancellationToken cancellationToken)
    {
        //получаем комментарий, к которому создается ответ, чтобы получить у него Id поста
        var parentComment = await _commentsRepository.GetByIdAsync(dto.ParComId, cancellationToken);

        var entity = new Comment
        {
            AccId=GetCurrentUserId(),
            UserName=GetCurrentUserName(),
            Content = dto.Content,
            PostId = parentComment.PostId,
            CreationDate = DateTime.UtcNow,
            ParComId = dto.ParComId,

        };

        return await _commentsRepository.CreateCommentAsync(entity, cancellationToken);
    }


    public async Task<List<CommentDto>> GetAnswersByCommentId(Guid commentId, CancellationToken cancellationToken)
    {
        List<Comment> entities =  _commentsRepository.GetAnswersByCommentId(commentId, cancellationToken).ToList();

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
                ParComId = entity.ParComId,
            });
        }
        return result;
    }
}

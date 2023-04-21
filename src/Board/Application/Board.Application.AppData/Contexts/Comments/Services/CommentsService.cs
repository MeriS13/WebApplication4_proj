using Board.Application.AppData.Contexts.Comments.Repositories;
using Board.Application.AppData.Contexts.Posts.Repositories;
using Board.Contracts.Comments;
using Board.Contracts.Posts;
using Board.Domain.Comments;
using Board.Domain.Posts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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


    ///<inheritdoc/>
    public async Task<Guid> CreateByPostIdAsync(CreateCommentDto dto, CancellationToken cancellationToken)
    {
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var claimName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new Exception ("Данная функция недоступна.");

        var UserId = Guid.Parse(claimId);

        // Преобр из дто в доменную модель
        var entity = new Comment
        {
            UserName = claimName,
            Content = dto.Content,
            CreationDate = DateTime.UtcNow,
            AccId = UserId,
            PostId = dto.PostId,
        };

        return await _commentsRepository.CreateByPostIdAsync (entity, cancellationToken);
    }


    ///<inheritdoc/>
    public Task DeleteByCommentId(Guid id, CancellationToken cancellationToken)
    {

        return _commentsRepository.DeleteByCommentId(id, cancellationToken);
    }


    ///<inheritdoc/>
    public async Task<List<CommentDto>> GetCommentsCarrentUserByIdAsync(CancellationToken cancellationToken)
    {

        List<Comment> entities = _commentsRepository.GetCommentsCarrentUserByIdAsync(cancellationToken).ToList();
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

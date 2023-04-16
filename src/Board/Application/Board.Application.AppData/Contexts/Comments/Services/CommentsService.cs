using Board.Application.AppData.Contexts.Comments.Repositories;
using Board.Contracts.Comments;
using Board.Domain.Comments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Comments.Services;

public class CommentsService : ICommentsService
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentsService(ICommentsRepository commentsRepository, IHttpContextAccessor httpContextAccessor)
    {
        _commentsRepository = commentsRepository;
        _httpContextAccessor = httpContextAccessor;
    }



    public async Task<Guid> CreateByPostIdAsync(CommentDto dto, CancellationToken cancellationToken)
    {
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) return null;

        var UserId = Guid.Parse(claimId);

        var entity = new Comment
        {
            Id = dto.Id,
            UserName = dto.UserName,
            Content = dto.Content,
            CreationDate = dto.CreationDate,
            AccId = UserId,
            PostId = dto.PostId,
            //остановилась тут

        };
    }

    public Task DeleteByCommentId(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<CommentDto>> GetCommentsCarrentUserByIdAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<CommentDto>> GetCommentsOnPostByIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

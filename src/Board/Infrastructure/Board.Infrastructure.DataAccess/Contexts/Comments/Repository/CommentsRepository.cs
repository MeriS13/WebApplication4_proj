using Board.Application.AppData.Contexts.Comments.Repositories;
using Board.Domain.Comments;
using Board.Domain.Posts;
using Board.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Board.Infrastructure.DataAccess.Contexts.Comments.Repository;

/// <inheritdoc cref="ICommentsRepository"/>
public class CommentsRepository : ICommentsRepository
{
    private readonly IRepository<Comment> _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentsRepository(IRepository<Comment> repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }


    /// <inheritdoc/>
    public async Task<Guid> CreateByPostIdAsync(Comment model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }


    /// <inheritdoc/>
    public async Task DeleteByCommentId(Guid commentId, CancellationToken cancellationToken)
    {
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new Exception("ClaimId is null!!!");

        var UserId = Guid.Parse(claimId);
        var claimName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        //Получение доменной модели Comment  и обработка исключений
        var entity = await _repository.GetByIdAsync(commentId, cancellationToken);

        if (entity == null) throw new Exception("Введеный идентификатор не принадлежит ни одному существующему комментарию!");
        if (entity.AccId != UserId && claimName != "Admin")
        {
            throw new Exception("Текущий пользователь не может удалить комментарий другого пользователя.");
        }

        await _repository.DeleteByIdAsync(commentId, cancellationToken);
    }


    /// <inheritdoc/>
    public IQueryable<Comment> GetCommentsCarrentUserByIdAsync(CancellationToken cancellationToken)
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(claimId)) throw new Exception("ClaimId is null!!!");
        var UserId = Guid.Parse(claimId);

        return _repository.GetAll(cancellationToken).Include(u => u.Account).Where(u => u.AccId == UserId);
    }


    /// <inheritdoc/>
    public IQueryable<Comment> GetCommentsOnPostByIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Include(u => u.Post).Where(u => u.PostId == postId);
    }
}

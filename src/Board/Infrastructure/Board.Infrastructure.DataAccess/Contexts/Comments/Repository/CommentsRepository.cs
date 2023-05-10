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

    public CommentsRepository(IRepository<Comment> repository)
    {
        _repository = repository;
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
        await _repository.DeleteByIdAsync(commentId, cancellationToken);
    }


    /// <inheritdoc/>
    public IQueryable<Comment> GetCommentsCurrentUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Include(u => u.Account).Where(u => u.AccId == userId);
    }


    /// <inheritdoc/>
    public IQueryable<Comment> GetCommentsOnPostByIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Include(u => u.Post).Where(u => u.PostId == postId);
    }

    /// <inheritdoc/>
    public async Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }
}

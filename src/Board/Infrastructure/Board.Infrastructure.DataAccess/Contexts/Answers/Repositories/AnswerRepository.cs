using Board.Application.AppData.Contexts.Answers.Repositories;
using Board.Domain.Answers;
using Board.Domain.Comments;
using Board.Domain.Posts;
using Board.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Answers.Repositories;

/// <inheritdoc cref="IAnswerRepository"/>
public class AnswerRepository : IAnswerRepository
{

    private readonly IRepository<Answer> _repository;

    public AnswerRepository(IRepository<Answer> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateByCommentIdAsync(Answer model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }

    /// <inheritdoc/>
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<Answer> GetAnswersOnCommentsById(Guid commentId, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Include(u => u.Comment).Where(u => u.CommentId == commentId);
    }

    /// <inheritdoc/>
    public async Task<Answer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }
}

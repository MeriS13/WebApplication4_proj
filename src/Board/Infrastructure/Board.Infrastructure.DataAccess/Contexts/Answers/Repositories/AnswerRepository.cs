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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AnswerRepository(IRepository<Answer> repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
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
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new Exception("ClaimId is null!!!");

        var UserId = Guid.Parse(claimId);
        var claimName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        //Получение доменной модели и обработка исключений
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new Exception("Введеный идентификатор не принадлежит ни одному существующему комментарию!");
        if (entity.AccId != UserId && claimName != "Admin")
        {
            throw new Exception("Текущий пользователь не может удалить комментарий другого пользователя.");
        }

        await _repository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<Answer> GetAnswersOnCommentsById(Guid commentId, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Include(u => u.Comment).Where(u => u.CommentId == commentId);
    }
}

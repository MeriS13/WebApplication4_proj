using Board.Application.AppData.Contexts.Answers.Repositories;
using Board.Application.AppData.Contexts.Comments.Repositories;
using Board.Contracts.Answers;
using Board.Contracts.Comments;
using Board.Domain.Answers;
using Board.Domain.Comments;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Answers.Services;

/// <inheritdoc cref="IAnswerService"/>
public class AnswerService : IAnswerService
{

    private readonly IAnswerRepository _answerRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AnswerService(IAnswerRepository answerRepository, IHttpContextAccessor httpContextAccessor)
    {
        _answerRepository = answerRepository;
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


    /// <inheritdoc/>
    public async Task<Guid> CreateByCommentIdAsync(CreateAnswerDto dto, CancellationToken cancellationToken)
    {

        var model = new Answer
        {
            UserName = GetCurrentUserName(),
            AccId = GetCurrentUserId(),
            Content = dto.Content,
            CommentId = dto.CommentId,
            CreationDate = DateTime.UtcNow,
        };

        return await _answerRepository.CreateByCommentIdAsync(model, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _answerRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new Exception("Введеный идентификатор не принадлежит ни одному существующему комментарию!");
        if (entity.AccId != GetCurrentUserId() && GetCurrentUserName() != "Admin")
        {
            throw new Exception("Текущий пользователь не может удалить комментарий другого пользователя.");
        }

        await _answerRepository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<AnswerDto>> GetAnswersOnCommentsById(Guid commentId, CancellationToken cancellationToken)
    {
        List<Answer> entities = _answerRepository.GetAnswersOnCommentsById(commentId, cancellationToken).ToList();
        List<AnswerDto> result = new();

        foreach (var entity in entities)
        {
            result.Add(new AnswerDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                CreationDate = entity.CreationDate,
                Content = entity.Content,
                AccId = entity.AccId,
                CommentId=entity.CommentId,
            });
        }
        return result;
    }
}

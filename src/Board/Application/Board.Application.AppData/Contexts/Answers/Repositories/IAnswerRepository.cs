using Board.Contracts.Answers;
using Board.Domain.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Answers.Repositories;

public interface IAnswerRepository
{
    /// <summary>
    /// Создать ответ к комменту по идентификаору коммента.
    /// </summary>
    /// <param name="dto"> Модель создания ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Идентификаор созданного ответа. </returns>
    Task<Guid> CreateByCommentIdAsync(Answer model, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление ответа по его идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> - </returns>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка ответов к комментарию.
    /// </summary>
    /// <param name="commentId"> Идентификатор комментария. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список ответов.(Доменных моделек) </returns>
    IQueryable<Answer> GetAnswersOnCommentsById(Guid commentId, CancellationToken cancellationToken);
}

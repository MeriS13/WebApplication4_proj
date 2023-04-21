using Board.Contracts.Answers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Answers.Services;

public interface IAnswerService
{
    /// <summary>
    /// Создать ответ к комменту по идентификаору коммента.
    /// </summary>
    /// <param name="dto"> Модель создания ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Идентификаор созданного ответа. </returns>
    Task<Guid> CreateByCommentIdAsync (CreateAnswerDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление ответа по его идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> - </returns>
    Task DeleteById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка ответов к комментарию.
    /// </summary>
    /// <param name="commentId"> Идентификатор комментария. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список ответов. </returns>
    Task<List<AnswerDto>> GetAnswersOnCommentsById(Guid commentId, CancellationToken cancellationToken);

    //Под вопросом
    //Task<IActionResult> GetAllMessageByPostId(Guid postId, CancellationToken cancellationToken);
}

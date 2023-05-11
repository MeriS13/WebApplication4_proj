using Board.Contracts.Answers;


namespace Board.Application.AppData.Contexts.Answers.Services;

public interface IAnswerService
{
    /// <summary>
    /// Метод для получения Id текущего пользователя из контекста
    /// </summary>
    /// <returns> Id текущего пользователя </returns>
    Guid GetCurrentUserId();

    /// <summary>
    /// Метод для получения имени текущего пользователя из контекста
    /// </summary>
    /// <returns> Имя текущего пользователя </returns>
    string GetCurrentUserName();


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

    /// <summary>
    /// Получение ответа по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Модель ответа </returns>
    Task<AnswerDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    //Под вопросом
    //Task<IActionResult> GetAllMessageByPostId(Guid postId, CancellationToken cancellationToken);
}

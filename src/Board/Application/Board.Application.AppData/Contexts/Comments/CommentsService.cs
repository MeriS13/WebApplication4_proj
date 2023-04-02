using Board.Contracts.Comments;

namespace Board.Application.AppData.Contexts.Comments;

/// <summary>
/// Сервис создания комментов
/// </summary>
public class CommentsService : ICommentsService
{
    /// <summary>
    /// Метод для создания комментов
    /// </summary>
    /// <param name="dto">Модель коммента</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Созданную модель коммента</returns>
    public async Task<CreateCommentDto> AddComment(CreateCommentDto dto, CancellationToken cancellationToken)
    {
        //вызов репозитория для сохранения в бд

        await Task.Run(() => dto, cancellationToken);


        return new CreateCommentDto();
    }
}
    //возможно должны быть другие методы для фактического взаимодействия,
    //работы с уже созданными данными, модельками, ValueObject(?) 
    //типа удаление редактирование. Тут идет вызов репозитория для выполнения CRUD операций в бд?
    //вызов Generic repository или каких-то частных репозиториев
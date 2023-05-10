using Board.Contracts.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Comments.Services;

/// <summary>
/// Сервис для работы с комментариямми.
/// </summary>
public interface ICommentsService
{
    /// <summary>
    /// Создание комментария.
    /// </summary>
    /// <param name="dto"> Модель создания комментария. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Идентификатор созданного коммента. </returns>
    Task<Guid> CreateByPostIdAsync (CreateCommentDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///  Удаление комментария по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор поста. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> - </returns>
    Task DeleteByCommentId (Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение комментариев е посту по идентификатору поста.
    /// </summary>
    /// <param name="postId"> Идентификатор поста. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список комментов </returns>
    Task<List<CommentDto>> GetCommentsOnPostByIdAsync (Guid postId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить комментарии, оставленные текущим пользователем.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список комментов </returns>
    Task<List<CommentDto>> GetCommentsCurrentUserByIdAsync (CancellationToken cancellationToken);

}

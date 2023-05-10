using Board.Contracts.Comments;
using Board.Domain.Answers;
using Board.Domain.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Comments.Repositories;

public interface ICommentsRepository
{
    /// <summary>
    /// Создание комментария.
    /// </summary>
    /// <param name="model"> Модель создания комментария. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Идентификатор созданного коммента. </returns>
    Task<Guid> CreateByPostIdAsync(Comment model, CancellationToken cancellationToken);

    /// <summary>
    ///  Удаление комментария по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор поста. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> - </returns>
    Task DeleteByCommentId(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение комментариев е посту по идентификатору поста.
    /// </summary>
    /// <param name="postId"> Идентификатор поста. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список комментов </returns>
    IQueryable<Comment> GetCommentsOnPostByIdAsync(Guid postId, CancellationToken cancellationToken);

    /// <summary>
    /// Получить комментарии, оставленные текущим пользователем.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <param name="id"> Идентификатор текущего пользователя </param>
    /// <returns> Список комментов </returns>
    IQueryable<Comment> GetCommentsCurrentUserByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение комментария по его идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор комментария </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Доменная модель комментария </returns>
    Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

using Board.Domain.Comments;
using Board.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Posts.Repositories;

/// <summary>
/// Репозиторий для работы с объявлениями.
/// </summary>
public interface IPostRepository
{
    /// <summary>
    /// Создание поста(объявления).
    /// </summary>
    /// <param name="dto">Модель создания поста</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Идентификатор созданного объявления</returns>
    Task<Guid> AddPostAsync(Post dto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка объявлений.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список объявлений </returns>
    IQueryable<Post> GetAll(CancellationToken cancellationToken);

    /// <summary>
    /// Получение объявления по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Модель поста </returns>
    Task<Post> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление объявления.
    /// </summary>
    /// <param name="dto"> Модель обновления обновления </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Обновленную модель объявления </returns>
    Task<Post> UpdateAsync(Guid id, Post dto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление объявления.
    /// </summary>
    /// <param name="id"> идентификатор объявления </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> - </returns>
    Task DeleteById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка постов, относящихся к категории.
    /// </summary>
    /// <param name="CategoryId"> Идентификатор категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список постов </returns>
    IQueryable<Post> GetAllPostsByCategoryId(Guid CategoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка постов для текущего аккаунта.
    /// </summary>
    /// <param name="UserId"> Идентификатор текущего аккаунта </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список постов </returns>
    IQueryable<Post> GetUserPosts(Guid UserId, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка постов по идентификатору родительской категории
    /// </summary>
    /// <param name="ParCatId"> Идентификатор родительской категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список доменных моделей постов </returns>
    IQueryable<Post> GetAllPostsByParentCategoryId(Guid ParCatId, CancellationToken cancellationToken);
}
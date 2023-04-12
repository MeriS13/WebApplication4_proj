using Board.Contracts.Comments;
using Board.Contracts.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Posts.Services;

/// <summary>
/// Сервис для работы с объявлениями
/// </summary>
/// <param name="dto">Модель создания объявления</param>
/// <param name="cancellationtoken">Токен отмены операции</param>
/// <returns> Модель объявления </returns>
public interface IPostService
{
    /// <summary>
    /// Создание поста(объявления).
    /// </summary>
    /// <param name="dto">Модель создания поста</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Идентификатор созданного объявления</returns>
    Task<Guid> CreatePostAsync(CreatePostDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка объявлений.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список объявлений </returns>
    Task<List<PostDto>> GetAll (CancellationToken cancellationToken);

    /// <summary>
    /// Получение объявления по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Модель поста </returns>
    Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление объявления.
    /// </summary>
    /// <param name="dto"> Модель обновления обновления </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Обновленную модель объявления </returns>
    Task<PostDto> UpdateAsync(Guid id, UpdatePostDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление объявления.
    /// </summary>
    /// <param name="id"> идентификатор объявления </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> - </returns>
    Task DeleteById(Guid id, CancellationToken cancellationToken);

    /*
    /// <summary>
    /// Получение комментариев, относящихся к объявлению.
    /// </summary>
    /// <param name="id"> Идентификатор объявления </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список комментариев </returns>
    Task<List<CommentDto>> GetAllCommentsByIdAsync(Guid id, CancellationToken cancellationToken);
    */


    /// <summary>
    /// Получение списка постов, относящихся к категории.
    /// </summary>
    /// <param name="CategoryId"> Идентификатор категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список постов </returns>
    Task<List<PostDto>> GetAllPostsByCategoryId(Guid CategoryId, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка постов для текущего аккаунта.
    /// </summary>
    /// <param name="cancellationToken"> токен отмены операции </param>
    /// <returns> Список постов </returns>
    Task<List<PostDto>> GetUserPosts(CancellationToken cancellationToken);

}


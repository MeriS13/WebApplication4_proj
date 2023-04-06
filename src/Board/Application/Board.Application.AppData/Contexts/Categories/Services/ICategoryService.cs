using Board.Contracts.Category;
using Board.Contracts.Posts;


namespace Board.Application.AppData.Contexts.Categories.Services;

/// <summary>
/// Интерфейс для работы с категориями.
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Создание категории.
    /// </summary>
    /// <param name="dto">Модель категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной категории</returns>
    Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение категории по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>информация о категории</returns>
    Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка категорий.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список категорий </returns>
    Task<List<CategoryDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Обновление категории.
    /// </summary>
    /// <param name="dto"> Модель обновления категории </param>
    /// <param name="cancellationToken"> токен отмены операции </param>
    Task<CategoryDto> UpdateAsync(UpdateCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление категории.
    /// </summary>
    /// <param name="id"> Идентификатор удаляемой категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns></returns>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка постов, относящихся к категории.
    /// </summary>
    /// <param name="CategoryId"> Идентификатор категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список постов </returns>
    Task<List<PostDto>> GetAllPosts(Guid CategoryId, CancellationToken cancellationToken);
}


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
    /// Создание родительской категории.
    /// </summary>
    /// <param name="dto">Модель создания родительской категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной категории</returns>
    Task<Guid> CreateParCatAsync(CreateParCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение категории по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>информация о категории</returns>
    Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получение списка категорий.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список категорий </returns>
    Task<List<CategoryInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Обновление категории.
    /// </summary>
    /// <param name="dto"> Модель обновления категории </param>
    /// <param name="cancellationToken"> токен отмены операции </param>
    Task<CategoryInfoDto> UpdateAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление категории.
    /// </summary>
    /// <param name="id"> Идентификатор удаляемой категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns></returns>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);


    Task<List<CategoryInfoDto>> GetParCatAsync (CancellationToken cancellationToken);



    /// <summary>
    /// Получение списка категорий, относящихся к одной родительской категории по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор родительской категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns></returns>
    Task<List<CategoryInfoDto>> GetCategoriesByParentIdAsync(Guid id, CancellationToken cancellationToken);
}


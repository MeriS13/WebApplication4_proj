using Board.Contracts.Category;


namespace Board.Application.AppData.Contexts.Categories.Services;

/// <summary>
/// Интерфейс для работы с категориями
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="dto">Модель категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной категории</returns>
    Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение категории по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>информация о категории</returns>
    Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);


}


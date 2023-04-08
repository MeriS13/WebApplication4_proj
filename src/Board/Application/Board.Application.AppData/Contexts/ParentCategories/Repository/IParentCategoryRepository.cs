
using Board.Domain.ParentCategories;


namespace Board.Application.AppData.Contexts.ParentCategories.Repository;

/// <summary>
/// Репозиторий для работы с родительскими категориями 
/// </summary>
public interface IParentCategoryRepository
{
    /// <summary>
    /// Создание родительской категории
    /// </summary>
    /// <param name="model"> Доменная модель род.категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Идентификатор созданной категории </returns>
    public Task<Guid> CreateAsync(ParentCategory model, CancellationToken cancellationToken);

    /// <summary>
    /// Получение родительской категории по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор родительской категории</param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Доменная модель родительской категории  </returns>
    public Task<ParentCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление родительской категории
    /// </summary>
    /// <param name="id"> Идентификатор категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns></returns>
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);


    /// <summary>
    /// Получение списка категорий.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список категорий </returns>
    IQueryable<ParentCategory> GetAll(CancellationToken cancellationToken);
}

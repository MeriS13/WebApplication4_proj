using Board.Contracts.Category;
using Board.Contracts.ParentCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.ParentCategories.Services;

/// <summary>
/// Репозиторий для работы с родительскими категориями
/// </summary>
public interface IParentCategoryService
{
    /// <summary>
    /// Получение списка род. категорий.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список категорий </returns>
    Task<List<ParentCategoryDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Создание родительской категории
    /// </summary>
    /// <param name="dto"> Модель создания род.категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Идентификатор созданной категории </returns>
    Task<Guid> CreateAsync(CreateParentCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение родительской категории по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор родительской категории</param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Родительскую категорию  </returns>
    Task<ParentCategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление родительской категории
    /// </summary>
    /// <param name="id"> Идентификатор категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns></returns>
    Task DeleteById(Guid id, CancellationToken cancellationToken);
}

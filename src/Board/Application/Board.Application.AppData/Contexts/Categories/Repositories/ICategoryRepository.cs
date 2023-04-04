using Board.Contracts.Category;
using Board.Contracts.Posts;
using Board.Domain.Categories;
using Board.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Categories.Repositories;


/// <summary>
/// Репозиторий для работы с категориями.
/// </summary>
public interface ICategoryRepository
{

    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="dto">Модель категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной категории</returns>
    Task<Guid> AddAsync(Category dto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение категории по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>информация о категории</returns>
    Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}

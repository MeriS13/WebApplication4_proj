using Board.Contracts.Category;

namespace Board.Application.AppData.Contexts.Categories;

/// <summary>
/// Сервис для работы с категориями
/// </summary>
public class CategoryService : ICategoryService
{
    /// <summary>
    /// Метод для создания категории
    /// </summary>
    /// <param name="dto">Модель категории</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    public async Task<CreateCategoryDto> AddCategory(CreateCategoryDto dto, CancellationToken cancellationToken)
    {

        //вызов репозитория для сохранения в бд

        await Task.Run(() => dto, cancellationToken);

        return new CreateCategoryDto();
    }

    //возможно должны быть другие методы для фактического взаимодействия,
    //работы с уже созданными данными, модельками, ValueObject(?) 
    //типа удаление редактирование. Тут идет вызов репозитория для выполнения CRUD операций в бд?
    //вызов Generic repository или каких-то частных репозиториев
}

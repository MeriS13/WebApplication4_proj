using Board.Contracts.Category;


namespace Board.Application.AppData.Contexts.Categories;

/// <summary>
/// Интерфейс для работы с категориями
/// </summary>
public interface ICategoryService
{
    // Создание категории
    Task<CreateCategoryDto> AddCategory(CreateCategoryDto dto, CancellationToken cancellationToken);


}


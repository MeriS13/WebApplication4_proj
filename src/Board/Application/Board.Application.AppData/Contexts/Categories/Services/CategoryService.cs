using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Contracts.Category;
using Board.Contracts.Posts;
using Board.Domain.Categories;
using Board.Domain.Posts;

namespace Board.Application.AppData.Contexts.Categories.Services;

/// <inheritdoc cref="ICategoryService"/>
/// Тут описываются методы соответствующие контроллеру
/// 
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    ///<inheritdoc /> все ок
    public Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        //Тут происходит маппинг. в сущность записывается дом.модель и заполняются ее поля
        //возвращаем в репозиторий доменную модель
        var entity = new Category
        {
            Name = dto.Name,
            ParentId = dto.ParentId,

        };
        return _categoryRepository.AddAsync(entity, cancellationToken);
    }

    ///<inheritdoc /> все ок
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        //return Task.FromResult(_categoryRepository.DeleteByIdAsync(id, cancellationToken));
        return _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }

    ///<inheritdoc /> все ок но вопрос с эвэйтом
    public async Task<List<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        //Получаем список доменных моделей, создаем список dto-моделей, в цикле добавляем
        //элементы списка - смаппленные модели к dto и возвращаем 
        List<Category> entities = _categoryRepository.GetAll(cancellationToken).ToList();
        List<CategoryDto> result = new();
        int i = 0;
        foreach (var entity in entities)
        {
            result.Add(new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = (Guid)entity.ParentId,

            });
        }
        return result;
    }

    ///<inheritdoc /> хз
    //подлежит проверке и все связанное с этим методом
    public async Task<List<PostDto>> GetAllPosts(Guid CategoryId, CancellationToken cancellationToken)
    {
        //получаем список домен.моделек постов
        List<Post> entities = _categoryRepository.GetAllPosts(CategoryId, cancellationToken).ToList();
        List<PostDto> result = new();
        int i = 0;
        foreach (var entity in entities)
        {
            result.Add(new PostDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreationDate = entity.CreationDate,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                IsFavorite = entity.IsFavorite,
            });
        }
        return result;
    }

    ///<inheritdoc /> все ок
    public async Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        //маппим доменную модельку в модель dto
        var result = new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentId = (Guid)entity.ParentId,

        };
        return result;
    }

    ///<inheritdoc /> все ок
    public async Task<CategoryDto> UpdateAsync( UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        //Преобразуем модель обновления к доменной
        var entity = new Category
        {
            Name = dto.Name,
            ParentId = dto.ParentId,

        };
        //отдаем обновленную доменную в репозиторий, там она обновляется в бд, возвращается она же
        var newModel = await _categoryRepository.UpdateAsync(entity, cancellationToken);
        //преобразуем обновленную доменную к модели категории
        var newDto = new CategoryDto
        {
            Name = newModel.Name,
            ParentId = (Guid)newModel.ParentId,
            Id = newModel.Id
        };
        return newDto;
    }


}

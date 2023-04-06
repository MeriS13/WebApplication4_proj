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

    ///<inheritdoc />
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

    ///<inheritdoc />
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        //return Task.FromResult(_categoryRepository.DeleteByIdAsync(id, cancellationToken));
        return _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }

    ///<inheritdoc />
    public IQueryable<CategoryDto> GetAll(CancellationToken cancellationToken)
    {
        //Получаем список доменных моделей, создаем список dto-моделей, в цикле маппим первые ко вторым и возвращаем 
        List<Category> entities = (List<Category>)_categoryRepository.GetAll(cancellationToken);
        List<CategoryDto> result = new List<CategoryDto>();
        for (int i = 0; i < entities.Count; i++)
        {
            result[i] = new CategoryDto
            {
                Id = entities[i].Id,
                Name = entities[i].Name,
                ParentId = (Guid)entities[i].ParentId,

            };
        }
        return (IQueryable<CategoryDto>)result;
    }


    public IQueryable<PostDto> GetAllPosts(Guid CategoryId, CancellationToken cancellationToken)
    {
        //получаем список домен.моделек постов
        List<Post> entities = (List<Post>)_categoryRepository.GetAllPosts(CategoryId, cancellationToken);
        List<PostDto> result = new List<PostDto>();
        for (int i = 0; i < entities.Count; i++)
        {
            result[i] = new PostDto
            {
                Id = entities[i].Id,
                Name = entities[i].Name,
                CreationDate = entities[i].CreationDate,
                Description = entities[i].Description,
                CategoryId = entities[i].CategoryId,
                IsFavorite = entities[i].IsFavorite,
            };
        }
        return (IQueryable<PostDto>)result;
    }

    ///<inheritdoc />
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

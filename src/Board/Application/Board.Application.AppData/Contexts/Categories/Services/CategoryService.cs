using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Contracts.Category;
using Board.Domain.Categories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;

namespace Board.Application.AppData.Contexts.Categories.Services;

/// <inheritdoc cref="ICategoryService"/>

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _memoryCache;

    public const string CategoryKey = "CategoryKey";

    public CategoryService(ICategoryRepository categoryRepository,IMemoryCache memoryCache)
    {
        _categoryRepository = categoryRepository;
        _memoryCache = memoryCache;
    }


    ///<inheritdoc /> 
    public async Task<Guid> CreateAsync(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        //Тут происходит маппинг. в сущность записывается дом.модель и заполняются ее поля
        var entity = new Category
        {
            Name = dto.Name,
            ParentId = dto.ParentId,
        };

        // Обработка исключения
        var existingCategory = await _categoryRepository.FindWhere(category => category.Name == dto.Name, cancellationToken);
        if (existingCategory != null)
        {
            throw new Exception($"Категория с названием '{dto.Name}' уже существует!");
        }

        return await _categoryRepository.AddAsync(entity, cancellationToken);
    }


    ///<inheritdoc /> 
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }


    ///<inheritdoc /> 
    public async Task<List<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(CategoryKey, out List<CategoryDto> result))
        {
            return result;
        }

        //Получаем список доменных моделей, создаем список dto-моделей, в цикле добавляем
        //элементы списка - смаппленные модели к dto и возвращаем 
        List<Category> entities = _categoryRepository.GetAll(cancellationToken).ToList();
        List<CategoryDto> result2 = new();
        foreach (var entity in entities)
        {
            result2.Add(new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = entity.ParentId,

            });
        }

        if (result2.IsNullOrEmpty()) { throw new Exception("Нет категорий :( "); };
        
         

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        _memoryCache.Set(CategoryKey, result2, options);

        return result2;
    }

    

    ///<inheritdoc /> 
    public async Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        
        var entity = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if(entity == null)
        {
            throw new Exception("Введеный идентификатор не принадлежит ни одной существующей категории!");
        }

        //маппим доменную модельку в модель dto
        var result = new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentId = (Guid)entity.ParentId,

        };


        return result;
    }

    ///<inheritdoc /> 
    public async Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (existingCategory == null)
        {
            throw new Exception("Введеный идентификатор не принадлежит ни одной существующей категории!");
        }

        //Преобразуем модель обновления к доменной
        var entity = new Category
        {
            Id = id,
            Name = dto.Name,
            ParentId = dto.ParentId,

        };
        //отдаем обновленную доменную в репозиторий, там она обновляется в бд, возвращается она же
        var newModel = await _categoryRepository.UpdateAsync(id, entity, cancellationToken);
        //преобразуем обновленную доменную к модели категории
        var newDto = new CategoryDto
        {
            Name = newModel.Name,
            ParentId = (Guid)newModel.ParentId,
            Id = newModel.Id
        };
        return newDto;
    }

    ///<inheritdoc /> 
    public async Task<List<CategoryDto>> GetCategoriesByParentIdAsync(Guid id, CancellationToken cancellationToken)
    {
        List<Category> entities = _categoryRepository.GetCategoriesByParentId(id, cancellationToken).ToList();
        List<CategoryDto> result = new();
        
        foreach (var entity in entities)
        {
            result.Add(new CategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = entity.ParentId
            });
        }

        if (result.IsNullOrEmpty())
        {
            throw new Exception("По указанному идентификатору родительской категории не найдены категории.");
        }
        return result;

    }
}

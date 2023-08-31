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
        //var existingCategory = await _categoryRepository.FindWhere(category => category.Name == dto.Name, cancellationToken);
        //if (existingCategory != null)
        //    return Guid.Parse("00000000-0000-0000-0000-000000000000");

        return await _categoryRepository.AddAsync(entity, cancellationToken);
    }


    public async Task<Guid> CreateParCatAsync(CreateParCategoryDto dto, CancellationToken cancellationToken)
    {
        //Тут происходит маппинг. в сущность записывается данные для дом.модель и заполняются ее поля
        var entity = new Category
        {
            Name = dto.Name,
            ParentId = Guid.Parse("00000000-0000-0000-0000-000000000000")
        };


        return await _categoryRepository.AddAsync(entity, cancellationToken);
    }

    //пррверить асинки аваиты и исправить контроллер
    public async Task<List<CategoryInfoDto>> GetParCatAsync(CancellationToken cancellationToken)
    {
        //var parentId = Guid.Parse("00000000-0000-0000-0000-000000000000");
        List<Category> entities = _categoryRepository.GetParCatAsync(cancellationToken).ToList();
        if (entities.IsNullOrEmpty()) return null;

        List<CategoryInfoDto> result = new();
        foreach (var entity in entities)
        {
            result.Add(new CategoryInfoDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = entity.ParentId,

            });
        }

        return result;
    }

    ///<inheritdoc /> 
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }


    ///<inheritdoc /> 
    public async Task<List<CategoryInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        if (_memoryCache.TryGetValue(CategoryKey, out List<CategoryInfoDto> result))
        {
            return result;
        }

        //Получаем список доменных моделей, создаем список dto-моделей, в цикле добавляем
        //элементы списка - смаппленные модели к dto и возвращаем 
        List<Category> entities = _categoryRepository.GetAll(cancellationToken).ToList();
        if (entities.IsNullOrEmpty()) return null;

        List<CategoryInfoDto> result2 = new();
        foreach (var entity in entities)
        {
            result2.Add(new CategoryInfoDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = entity.ParentId,

            });
        }
 
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        _memoryCache.Set(CategoryKey, result2, options);
        

        return result2;
    }

    

    ///<inheritdoc /> 
    public async Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if(entity == null)
        {
            return null;
        }

        var result = new CategoryInfoDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentId = (Guid)entity.ParentId,

        };

        return result;
    }

    ///<inheritdoc /> 
    public async Task<CategoryInfoDto> UpdateAsync(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (existingCategory == null)
            return null;
        

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
        var newDto = new CategoryInfoDto
        {
            Name = newModel.Name,
            ParentId = (Guid)newModel.ParentId,
            Id = newModel.Id
        };
        return newDto;
    }

    ///<inheritdoc /> 
    public async Task<List<CategoryInfoDto>> GetCategoriesByParentIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entities = _categoryRepository.GetCategoriesByParentId(id, cancellationToken);

        if (entities.IsNullOrEmpty()) return null;

        List<CategoryInfoDto> result = new();
        foreach (var entity in entities)
        {
            result.Add(new CategoryInfoDto
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = entity.ParentId,

            });
        }

        return result;
    }

}

using Board.Application.AppData.Contexts.ParentCategories.Repository;
using Board.Contracts.ParentCategory;
using Board.Domain.ParentCategories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;


namespace Board.Application.AppData.Contexts.ParentCategories.Services;



/// <inheritdoc cref="IParentCategoryService"/>

public class ParentCategoryService : IParentCategoryService
{
    private readonly IParentCategoryRepository _parentCategoryRepository;
    private readonly IMemoryCache _memoryCache;

    public const string parCatKey = "ParentCategoryKey";

    public ParentCategoryService(IParentCategoryRepository parentCategoryRepository, IMemoryCache memoryCache)
    {
        _parentCategoryRepository = parentCategoryRepository;
        _memoryCache = memoryCache;
    }

    /// <inheritdoc/>
    public async Task<List<ParentCategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        
        if (_memoryCache.TryGetValue(parCatKey, out List<ParentCategoryDto>  result))
        {
            return result;
        }

        var entities = _parentCategoryRepository.GetAll(cancellationToken).ToList();
        if (entities.IsNullOrEmpty()) return null;

        List<ParentCategoryDto> result2 = new List<ParentCategoryDto>();    
        foreach (var entity in entities)
        {
            result2.Add(new ParentCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
            });
        }
                
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
         _memoryCache.Set(parCatKey, result2, options);

        return result2;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(CreateParentCategoryDto dto, CancellationToken cancellationToken)
    {
        // Обработка исключения
        var existingCategory = await _parentCategoryRepository.FindWhere(category => category.Name == dto.Name, cancellationToken);
        if (existingCategory != null)
        {
            throw new Exception($"Категория с названием '{dto.Name}' уже существует!");
        }

        var entity = new ParentCategory
        {
            Name = dto.Name
        };
        Guid id = await _parentCategoryRepository.CreateAsync(entity, cancellationToken);
        return id;
    }

    /// <inheritdoc/>
    public Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        return _parentCategoryRepository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ParentCategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {

        var entity = await _parentCategoryRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null) return null;

        var result = new ParentCategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };
        return result;
    }
}

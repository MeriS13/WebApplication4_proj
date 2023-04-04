using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Contracts.Category;
using Board.Domain.Categories;

namespace Board.Application.AppData.Contexts.Categories.Services;

/// <inheritdoc cref="ICategoryService"/>
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
        var entity = new Category
        {
            Name = dto.Name,
            ParentId = dto.ParentId,

        };
        return _categoryRepository.AddAsync(entity, cancellationToken);
    }   

    ///<inheritdoc />
    public async Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        var result = new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentId = (Guid)entity.ParentId,

        };
        return result;
    }
}

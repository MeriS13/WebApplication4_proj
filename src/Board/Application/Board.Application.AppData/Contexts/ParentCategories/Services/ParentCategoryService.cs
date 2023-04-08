using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Application.AppData.Contexts.ParentCategories.Repository;
using Board.Contracts.Category;
using Board.Contracts.ParentCategory;
using Board.Domain.Categories;
using Board.Domain.ParentCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.ParentCategories.Services;

/// <inheritdoc cref="IParentCategoryService"/>

public class ParentCategoryService : IParentCategoryService
{
    private readonly IParentCategoryRepository _parentCategoryRepository;

    public ParentCategoryService(IParentCategoryRepository parentCategoryRepository)
    {
        _parentCategoryRepository = parentCategoryRepository;
    }

    /// <inheritdoc/>
    public async Task<List<ParentCategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        //Получаем список доменных моделей, создаем список dto-моделей, в цикле добавляем
        //элементы списка - смаппленные модели к dto и возвращаем 
        List<ParentCategory> entities = _parentCategoryRepository.GetAll(cancellationToken).ToList();
        List<ParentCategoryDto> result = new();
        foreach (var entity in entities)
        {
            result.Add(new ParentCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name,
            });
        }
        return result;
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(CreateParentCategoryDto dto, CancellationToken cancellationToken)
    {
        var entity = new ParentCategory
        {
            //Id = dto.Id,
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

        var result = new ParentCategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
        };
        return result;
    }
}

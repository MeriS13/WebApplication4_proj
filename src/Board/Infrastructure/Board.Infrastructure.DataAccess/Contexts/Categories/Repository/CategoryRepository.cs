using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Domain.Categories;
using Board.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Categories.Repository;

/// <inheritdoc cref="ICategoryRepository"/>

public class CategoryRepository : ICategoryRepository
{
    private readonly IRepository<Category> _repository;

    public CategoryRepository(IRepository<Category> repository)
    {
        _repository = repository;
    }

    ///<inheritdoc/>
    public async Task<Guid> AddAsync(Category dto, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(dto, cancellationToken);
        return dto.Id;
    }

    ///<inheritdoc/>
    public Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }
}

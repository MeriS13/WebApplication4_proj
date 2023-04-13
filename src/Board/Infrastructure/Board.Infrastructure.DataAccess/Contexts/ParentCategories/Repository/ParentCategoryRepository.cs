using Board.Application.AppData.Contexts.ParentCategories.Repository;
using Board.Domain.Categories;
using Board.Domain.ParentCategories;
using Board.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.ParentCategories.Repository;

/// <inheritdoc cref="IParentCategoryRepository"/>
public class ParentCategoryRepository : IParentCategoryRepository
{
    private readonly IRepository<ParentCategory> _repository;

    public ParentCategoryRepository(IRepository<ParentCategory> repository)
    {
        _repository = repository;
    }


    /// <inheritdoc/>
    public async Task<Guid> CreateAsync(ParentCategory model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }

    /// <inheritdoc/>
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ParentCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<ParentCategory> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ParentCategory> FindWhere(Expression<Func<ParentCategory, bool>> predicate, CancellationToken cancellation)
    {
        var data = _repository.GetAllFiltered(predicate);

        ParentCategory category = await data.Where(predicate).FirstOrDefaultAsync(cancellation);

        return category;
    }
}

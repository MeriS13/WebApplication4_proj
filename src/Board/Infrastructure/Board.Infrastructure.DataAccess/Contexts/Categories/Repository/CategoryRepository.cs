using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Contracts.Category;
using Board.Contracts.Posts;
using Board.Domain.Accounts;
using Board.Domain.Categories;
using Board.Domain.Posts;
using Board.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public async Task<Guid> AddAsync(Category model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }

    ///<inheritdoc/> 
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(id, cancellationToken);    
    }

    ///<inheritdoc/> (вернет dbset, являющийся реализаций IQueryable).
    public IQueryable<Category> GetAll(CancellationToken cancellationToken)
    {
        return  _repository.GetAll(cancellationToken);
    }

    
    ///<inheritdoc/> 
    public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    ///<inheritdoc/>  
    public async Task<Category> UpdateAsync(Guid id, Category dto, CancellationToken cancellationToken)
    {
        var model = await _repository.UpdateAsync(id, dto, cancellationToken);
        return model;
    }

    ///<inheritdoc/>
    public IQueryable<Category> GetCategoriesByParentId(Guid id, CancellationToken cancellationToken)
    {

        return _repository.GetAll(cancellationToken).Where(u => u.ParentId == id);
    }

    /// <inheritdoc/>
    public async Task<Category> FindWhere(Expression<Func<Category, bool>> predicate, CancellationToken cancellation)
    {
        var data = _repository.GetAllFiltered(predicate);

        Category category = await data.Where(predicate).FirstOrDefaultAsync(cancellation);

        return category;
    }


    public IQueryable<Category> GetParCatAsync(CancellationToken cancellationToken)
    {
        var parentId = Guid.Parse("00000000-0000-0000-0000-000000000000");
        return  _repository.GetAll(cancellationToken).Where(u => u.ParentId == parentId);
    }

   
}

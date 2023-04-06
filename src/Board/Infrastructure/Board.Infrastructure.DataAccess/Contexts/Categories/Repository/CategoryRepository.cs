using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Contracts.Posts;
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


    ///<inheritdoc/>тут все ок
    public async Task<Guid> AddAsync(Category dto, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(dto, cancellationToken);
        return dto.Id;
    }

    ///<inheritdoc/> тут тоже все ок
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(id, cancellationToken);    
    }

    ///<inheritdoc/> все ок
    public IQueryable<Category> GetAll(CancellationToken cancellationToken)
    {
        return  _repository.GetAll(cancellationToken);
    }

    ///<inheritdoc/> ????????????? мб неправильно
    public IQueryable<Post> GetAllPosts(Guid CategoryId, CancellationToken cancellationToken)
    {
        //return await _repository.GetAllFiltered(); тут по примеру изэктив
        //получаем связанные данные по навигацион.св-ву. Получаем список доменных моделек постов
        var resultList =  _repository.GetAll(cancellationToken).Include(u => u.Posts).ToList();
        return (IQueryable<Post>)resultList;
    }

    ///<inheritdoc/> все ок
    public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    ///<inheritdoc/> все ок (вроде)
    public async Task<Category> UpdateAsync(Category dto, CancellationToken cancellationToken)
    {
        var model = await _repository.UpdateAsync(dto, cancellationToken);
        return model;
    }
}

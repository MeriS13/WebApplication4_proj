using Board.Application.AppData.Contexts.Posts.Repositories;
using Board.Contracts.Posts;
using Board.Domain.Categories;
using Board.Domain.Comments;
using Board.Domain.Posts;
using Board.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace Board.Infrastructure.DataAccess.Contexts.Posts.Repositories;

/// <inheritdoc cref="IPostRepository"/>
public class PostRepository : IPostRepository
{
    private readonly IRepository<Post> _repository;

    public PostRepository(IRepository<Post> postRepository)
    {
        _repository = postRepository;
    }



    /// <inheritdoc/>
    public async Task<Guid> AddPostAsync(Post model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }

    /// <inheritdoc/>
    public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {

        await _repository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<Post> GetAll(CancellationToken cancellationToken)
    {
        return  _repository.GetAll(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Post> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    ///<inheritdoc/> подлежит проверке
    public IQueryable<Post> GetAllPostsByCategoryId(Guid CategoryId, CancellationToken cancellationToken)
    {
        //получаем связанные данные по навигацион.св-ву. Получаем список доменных моделек постов
        return _repository.GetAll(cancellationToken).Include(u => u.Category).Where(u => u.CategoryId == CategoryId);
    }

    /// <inheritdoc/>
    public async Task<Post> UpdateAsync(Guid id, Post dto, CancellationToken cancellationToken)
    {
        var model = await _repository.UpdateAsync(id, dto, cancellationToken);
        return model;
    }

    /// <inheritdoc/>
    public IQueryable<Post> GetUserPosts(Guid UserId, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Include(u => u.Account).Where(u => u.AccountId == UserId);
    }

    /// <inheritdoc/>
    public IQueryable<Post> GetAllPostsByParentCategoryId(Guid ParCatId, CancellationToken cancellationToken)
    {
        //получаем связанные данные по навигацион.св-ву. Получаем список доменных моделек постов
        return _repository.GetAll(cancellationToken)
            .Include(u => u.Category.ParentCategory).Where(u => u.Category.ParentCategory.Id == ParCatId);

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected DbContext DbContext { get; }
    protected DbSet<TEntity> DbSet { get; }

    public Repository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        await DbSet.AddAsync(model);
        await DbContext.SaveChangesAsync();

    }

    public async Task DeleteAsync(TEntity model)
    {
        if(model == null) throw new ArgumentNullException( nameof(model));
        DbSet.Remove(model);
        await DbContext.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predicat)
    {
        if(predicat == null) throw new ArgumentNullException(nameof(predicat));
        return DbSet.Where(predicat);
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        DbSet.Update(model);
        await DbContext.SaveChangesAsync();
    }
}
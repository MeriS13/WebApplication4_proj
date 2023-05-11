using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Board.Infrastructure.Repository;

/// <inheritdoc />
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected DbContext DbContext { get; }
    protected DbSet<TEntity> DbSet { get; }

    /// <inheritdoc />
    public Repository(DbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetAll(CancellationToken cancellationToken)
    {
        return DbSet;
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> predicat)
    {
        if (predicat == null) throw new ArgumentNullException(nameof(predicat));
        return DbSet.Where(predicat);
    }

    /// <inheritdoc />
    public async Task AddAsync(TEntity model, CancellationToken cancellationToken)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        await DbSet.AddAsync(model, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);

    }

    /// <inheritdoc />
    public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await DbSet.FindAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> UpdateAsync(Guid id, TEntity model, CancellationToken cancellationToken)
    {

        if (model == null) throw new ArgumentNullException(nameof(model));
        if (await DbSet.FindAsync(id) is TEntity foundModel)
        {
            DbSet.Entry(foundModel).CurrentValues.SetValues(model);

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        return await DbSet.FindAsync(id, cancellationToken); 
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var model = await DbSet.FindAsync(id, cancellationToken);
        
        DbSet.Remove(model);
        await DbContext.SaveChangesAsync(cancellationToken);
    }

}
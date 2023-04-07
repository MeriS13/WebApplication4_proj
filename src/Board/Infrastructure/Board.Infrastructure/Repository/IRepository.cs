using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.Repository;

public interface IRepository<TEntity> where TEntity : class
{

    IQueryable<TEntity> GetAll(CancellationToken cancellationToken);

    IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter);

    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(TEntity model, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity model, CancellationToken cancellationToken);

    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}

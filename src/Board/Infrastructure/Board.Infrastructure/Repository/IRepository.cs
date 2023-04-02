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

    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAllFiltered(Expression<Func<TEntity, bool>> filter);

    Task<TEntity> GetByIdAsync(Guid id);

    Task AddAsync(TEntity model);

    Task UpdateAsync(TEntity model);

    Task DeleteAsync(TEntity model);
}

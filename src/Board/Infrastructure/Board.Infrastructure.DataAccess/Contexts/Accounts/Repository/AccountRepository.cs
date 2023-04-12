using Board.Application.AppData.Contexts.Accounts.Repositories;
using Board.Domain.Accounts;
using Board.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Accounts.Repository;

/// <inheritdoc cref="IAccountRepository"/>
public class AccountRepository : IAccountRepository
{
    private readonly IRepository<Account> _repository;

    
    public AccountRepository(IRepository<Account> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddAsync(Account model, CancellationToken cancellation)
    {
        await _repository.AddAsync(model, cancellation);
        return model.Id;
    }

    /// <inheritdoc/>
    public Task<Account> FindById(Guid id, CancellationToken cancellation)
    {
        return _repository.GetByIdAsync(id, cancellation);
    }

    /// <inheritdoc/>
    public async Task<Account> FindWhere(Expression<Func<Account, bool>> predicate, CancellationToken cancellation)
    {
        var data = _repository.GetAllFiltered(predicate);

        Account account = await data.Where(predicate).FirstOrDefaultAsync(cancellation);

        return account;
    }
}

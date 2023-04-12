using Board.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Accounts.Repositories;

public interface IAccountRepository
{
    /// <summary>
    /// Поиск пользователя по фильтру.
    /// </summary>
    /// <param name="predicate"> Предикат </param>
    /// <param name="cancellation"> Токен отмены операции </param>
    /// <returns> Доменную модель аккаунта </returns>
    Task<Account> FindWhere(Expression<Func<Account, bool>> predicate, CancellationToken cancellation);

    /// <summary>
    /// Поиск пользователя по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор пользователя </param>
    /// <param name="cancellation"> Токен отмены операции </param>
    /// <returns> Доменную модель аккаунта </returns>
    Task<Account> FindById(Guid id, CancellationToken cancellation);

    /// <summary>
    /// Добавление пользователя.
    /// </summary>
    /// <param name="entity"> Доменная модель пользователя.</param>
    /// <param name="cancellation"> Токен отмены операции </param>
    /// <returns> Идентификатор добавленного пользователя </returns>
    Task<Guid> AddAsync(Account entity, CancellationToken cancellation);
}

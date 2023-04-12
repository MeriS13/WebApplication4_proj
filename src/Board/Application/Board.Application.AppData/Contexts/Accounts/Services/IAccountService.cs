
using Board.Contracts.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Accounts.Services;

/// <summary>
/// Сервис для регистрации пользователя
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Регистрация пользователя.
    /// </summary>
    /// <param name="accountDto"> Модель создания аккаунта </param>
    /// <param name="cancellation">Токен отмены.</param>
    /// <returns>Идентификатор пользователя.</returns>
    Task<Guid> RegisterAccountAsync(CreateAccountDto accountDto, CancellationToken cancellation);

    /// <summary>
    /// Авторизация пользователя.
    /// </summary>
    /// <param name="accountDto"> Модель входа в аккаунта </param>
    /// <param name="cancellation">Токен отмены.</param>
    /// <returns> Jwt-токен.</returns>
    Task<string> LoginAsync(LoginAccountDto accountDto, CancellationToken cancellation);

    /// <summary>
    /// Получение текущего пользователя.
    /// </summary>
    /// <param name="cancellation">Токен отмены.</param>
    /// <returns>Текущий пользователь.</returns>
    Task<AccountDto> GetCurrentAsync(CancellationToken cancellation);
}

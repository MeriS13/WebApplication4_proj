using Board.Contracts;
using Board.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с аккаунтами.
/// </summary>

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AccountController"/>
    /// </summary>
    /// <param name="logger">Сервис логирования.</param>
    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Зарегистрировать новый аккаунт.
    /// </summary>
    /// <param name="dto">Модель регистрации аккаунта.</param>
    /// <param name="cancellation">Токен отмены.</param>
    /// <returns>Модель зарегистрированного аккаунта.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterAccount([FromBody] CreateAccountDto dto, CancellationToken cancellation)
    {
        _logger.LogInformation("Регистрация нового аккаунта.");

        return await Task.Run(() => CreatedAtAction(nameof(Login), Guid.Empty), cancellation);
    }

    /// <summary>
    /// Войти в аккаунт.
    /// </summary>
    /// <param name="dto">Модель входа в аккаунт.</param>
    /// <param name="cancellation">Токен отмены.</param>
    /// <returns>Модель с данными входа.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAccountDto dto, CancellationToken cancellation)
    {
        _logger.LogInformation("Вход в аккаунт.");

        return await Task.Run(() => Ok(new LoginAccountDto()), cancellation);
    }
}
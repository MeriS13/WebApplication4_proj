using Board.Application.AppData.Contexts.Accounts.Services;
using Board.Contracts;
using Board.Contracts.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с аккаунтами.
/// </summary>

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AccountController"/>
    /// </summary>
    /// <param name="logger">Сервис логирования.</param>
    public AccountController(ILogger<AccountController> logger, IAccountService accountService = null)
    {
        _logger = logger;
        _accountService = accountService;
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

        var result = await _accountService.RegisterAccountAsync(dto, cancellation);
        return await Task.Run(() => CreatedAtAction(nameof(Login), result), cancellation);
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

        var result = await _accountService.LoginAsync(dto, cancellation);

        return await Task.Run(() => Ok(result), cancellation);
    }

    [HttpPost("logout")]
    public async Task Logout(string token)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpPost("GetUserInfo")]
    public async Task<AccountDto> GetUserInfo(CancellationToken cancellation)
    {
        var result = await _accountService.GetCurrentAsync(cancellation);

        return result;
    }

}
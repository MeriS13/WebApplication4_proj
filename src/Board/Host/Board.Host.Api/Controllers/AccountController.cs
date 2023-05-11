using Board.Application.AppData.Contexts.Accounts.Services;
using Board.Contracts.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с аккаунтами.
/// </summary>

[ApiController]
[Route(template: "account")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAccountService _accountService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AccountController"/>
    /// </summary>
    /// <param name="logger">Сервис логирования.</param>
    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
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
    /// <response code="201"> Пользователь зарегестрирован. </response>
    /// <response code="400"> Введенные данные содержат запрещенные слова. </response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
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
    /// <response code="200"> Аутентификация прошла успешно. </response>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginAccountDto dto, CancellationToken cancellation)
    {
        _logger.LogInformation("Вход в аккаунт.");

        var result = await _accountService.LoginAsync(dto, cancellation);
        if (result == null) return StatusCode((int)HttpStatusCode.BadRequest);

        return await Task.Run(() => Ok(result), cancellation);
    }

    /// <summary>
    /// Получить информацию о текущем пользователе
    /// </summary>
    /// <param name="cancellation">Токен отмены.</param>
    /// <returns> Модель аккаунта </returns>
    /// <response code="200"> Информация об аккаунте. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpPost("GetUserInfo")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<AccountDto> GetUserInfo(CancellationToken cancellation)
    {
        var result = await _accountService.GetCurrentAsync(cancellation);

        return result;
    }

}
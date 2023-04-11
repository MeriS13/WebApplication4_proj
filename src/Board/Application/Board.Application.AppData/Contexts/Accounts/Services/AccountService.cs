using Board.Application.AppData.Contexts.Accounts.Repositories;
using Board.Contracts.Account;
using Board.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Board.Application.AppData.Contexts.Accounts.Services;

/// <inheritdoc cref="IAccountService" />
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _сonfiguration;

    public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccesso, IConfiguration сonfiguration)
    {
        _accountRepository = accountRepository;
        _httpContextAccessor = httpContextAccesso;
        _сonfiguration = сonfiguration;
    }

    /// <inheritdoc />
    public async Task<Guid> RegisterAccountAsync(CreateAccountDto accountDto, CancellationToken cancellation)
    {
        var account = new Account
        {
            Name = accountDto.Login,
            Login = accountDto.Login,
            Password = accountDto.Password,
            Created = DateTime.UtcNow
        };

        //Проверка наличия пользователя. Если найден - ошибка(такой юзер уже есть), если нет - добавляем
        var existingAccount = await _accountRepository.FindWhere(account => account.Login == accountDto.Login, cancellation);
        if (existingAccount != null)
        {
            throw new Exception($"Пользователь с логином '{accountDto.Login}' уже зарегистрирован!");
        }

        await _accountRepository.AddAsync(account, cancellation);

        return account.Id;
    }

    /// <inheritdoc />
    public async Task<string> LoginAsync(LoginAccountDto accountDto, CancellationToken cancellation)
    {
        //проверка наличия аккаунта в бд по логину
        var existingAccount = await _accountRepository.FindWhere(account => account.Login == accountDto.Login, cancellation);
        if (existingAccount == null)
        {
            throw new Exception("Пользователь не найден!");
        }

        //проверка совпадения пароля
        if (!existingAccount.Password.Equals(accountDto.Password))
        {
            throw new Exception("Неверный логин или пароль.");
        }

        //получаем клеймы
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, existingAccount.Id.ToString()),
            new Claim(ClaimTypes.Name, existingAccount.Login)
        };

        //получаем секрет из конфигурации
        var secretKey = _сonfiguration["Jwt:Key"];

        //собираем токен
        var token = new JwtSecurityToken
            (
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                SecurityAlgorithms.HmacSha256
                )
            );

        var result = new JwtSecurityTokenHandler().WriteToken(token);

        return result;
    }

    /// <inheritdoc />
    public async Task<AccountDto> GetCurrentAsync(CancellationToken cancellation)
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId))
        {
            return null;
        }

        var id = Guid.Parse(claimId);
        var user = await _accountRepository.FindById(id, cancellation);

        if (user == null)
        {
            throw new Exception($"Не найден пользователь с идентификатором '{id}'.");
        }

        //TODO
        var result = new AccountDto
        {
            Id = user.Id,
            Login = user.Login,

        };

        return result;
    }
}
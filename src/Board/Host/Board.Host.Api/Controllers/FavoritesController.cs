using Board.Application.AppData.Contexts.Favorites;
using Board.Application.AppData.Contexts.Posts.Services;
using Board.Contracts.Favotites;
using Board.Contracts.Posts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Board.Host.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route(template: "favorites-controller")]
public class FavoritesController : ControllerBase
{

    private readonly IFavoritesService _favoritesService;
    private readonly ILogger<FavoritesController> _logger;

    /// <summary>
    /// Инициализация.
    /// </summary>
    /// <param name="favoritesService">Сервис для работы с Избранным</param>
    /// <param name="logger">Логгер</param>
    public FavoritesController(IFavoritesService favoritesService, ILogger<FavoritesController> logger)
    {
        _favoritesService = favoritesService;
        _logger = logger;
    }



    /// <summary>
    /// Добавление объявления в Избранное
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns></returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> AddToFavorites(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Добавление в Избранное");

        return await Task.Run(() => Ok(new FavoriteDto()), cancellationToken);
    }

    ///<summary>
    /// Получение списка обьявлений в избранном
    /// template - для определения маршрута
    ///</summary>
    [HttpGet(template: "get-favorites")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка избранного");

        return await Task.Run(() => Ok(Enumerable.Empty<FavoriteDto>()), cancellationToken);
    }

    /// <summary>
    /// Удаление объявления из избранного по параметру id
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteFavoriteById (Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление из избранного");
        return await Task.Run(NoContent, cancellationToken);
    }
}

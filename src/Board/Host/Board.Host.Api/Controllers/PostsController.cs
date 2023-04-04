using Microsoft.AspNetCore.Mvc;
using Board.Contracts.Posts;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Board.Application.AppData.Contexts.Posts.Services;

namespace Board.Host.Api.Controllers;

///<summary>
///Api-контроллер для работы с постами(объявлениями) 
/// имеет логгер
///</summary>

[ApiController]
[Route(template: "posts-controller")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ILogger<PostsController> _logger;

    /// <summary>
    /// Инициализирует экземпляр <see cref="PostsController"/>
    /// </summary>
    /// <param name="logger">сервис логгирования</param>
    /// <param name="postService"> сервис для работы с объявлениями</param>
    public PostsController(ILogger<PostsController> logger, IPostService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    ///<summary>
    /// Получение списка обьявлений
    /// template - для определения маршрута
    ///</summary>
    [HttpGet(template: "get-posts")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос объявлений");

        return await Task.Run(() => Ok(Enumerable.Empty<PostDto>()), cancellationToken);
    }

    /// <summary>
    /// Получить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель объявления.</returns>
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await Task.Run(() => Ok(new PostDto()), cancellationToken);
    }

    ///<summary>
    /// Сохраняет новое объявление 
    ///</summary>
    ///<param name="dto"> Модель создания объявления </param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    ///<returns> Модель созданного объявления </returns>

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message: $"{JsonConvert.SerializeObject(dto)}");

        var result = await _postService.AddPost(dto, cancellationToken);

        return await Task.Run(function: ()=> Created(uri: string.Empty, result));
    }

    /// <summary>
    /// Частично обновить объявление.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="dto">Модель обновления объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель обновлённого объявления.</returns>
    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> Patch(Guid id, [FromBody] UpdatePostDto dto, CancellationToken cancellationToken)
    {
        return await Task.Run(() => Ok(new PostDto()), cancellationToken);
    }

    /// <summary>
    /// Удаление объявления по параметру id
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        return await Task.Run(NoContent, cancellationToken);
    }

    
}

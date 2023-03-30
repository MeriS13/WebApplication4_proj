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
    /// Гет-запрос. получение данных 
    /// template - для определения маршрута
    ///</summary>
    [HttpGet(template: "get-posts")]
    public async Task<IActionResult> Get() // возвр результат задачи? типа делегата айэкшнрезалт
    {
        _logger.LogInformation(message: $"Запрос объявления"); //для получ логов в консоли
        return await Task.Run(Ok); //возвращает код 200 -ок
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
    /// Удаление объявления по параметру id?
    /// </summary>
    /// <returns></returns>
    [HttpDelete]    
    public async Task<IActionResult> DeletePost()
    {
        
        _logger.LogInformation(message: $"Удаление объявления");
        return await Task.Run(Ok);
    }
      
    
}

using Microsoft.AspNetCore.Mvc;
using Board.Contracts.Posts;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Board.Application.AppData.Contexts.Posts.Services;
using Board.Domain.Categories;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace Board.Host.Api.Controllers;

///<summary>
///Api-контроллер для работы с постами(объявлениями) 
/// имеет логгер
///</summary>

[ApiController]
[Route(template: "posts-controller")]
[AllowAnonymous]
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

    /// <summary>
    /// Получение списка обьявлений
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список постов </returns>
    [HttpGet(template: "get-posts")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос объявлений");
        var result = _postService.GetAll(cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    /// <summary>
    /// Получить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель объявления.</returns>
    [HttpGet("лалала")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос получение объявления по идентификатору");
        var result = await _postService.GetByIdAsync(id, cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    ///<summary>
    /// Сохраняет новое объявление 
    ///</summary>
    ///<param name="dto"> Модель создания объявления </param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    ///<returns> Модель созданного объявления </returns>

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message: $"Сохранение объявления {JsonConvert.SerializeObject(dto)}");
        var result = await _postService.CreatePostAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Частично обновить объявление.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="dto">Модель обновления объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Модель обновлённого объявления.</returns>
    [HttpPut("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostDto dto, CancellationToken cancellationToken)
    {
        var result = await _postService.UpdateAsync(id, dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Удаление объявления по параметру id
    /// </summary>
    /// <returns> StatusCode </returns>
    [HttpDelete("{id:Guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await _postService.DeleteById(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }
    


    /// <summary>
    /// Получить список постов по идентификатору категории.
    /// </summary>  где-то в сервисе преобразование не происходит
    /// <param name="CategoryId"> Идентификатор категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список постов </returns>
    [HttpGet("{CategoryId:Guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllPostsByCategoryId(Guid CategoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка постов для категории");
        var result = _postService.GetAllPostsByCategoryId(CategoryId, cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    /// <summary>
    /// Получить список постов текущего пользователя
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns> Список постов </returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPostsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка постов для текущего аккаунта");
        var result = _postService.GetUserPosts(cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    /// <summary>
    /// Получить список постов, относящихся к определенной родительской категории по ее Id
    /// </summary>
    /// <param name="ParCatId"> Идентификатор родительской категории </param>
    /// <param name="cancellationToken"> токен отмены операции </param>
    /// <returns> Список постов </returns>
    [HttpGet("GetAllPostsByParentCategoryId")]
    [Authorize]
    public async Task<IActionResult> GetAllPostsByParentCategoryId(Guid ParCatId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка постов по Id родительской категории для категории, к которой относится пост");
        var result = _postService.GetAllPostsByParentCategoryId(ParCatId, cancellationToken);
        return await Task.Run(() => Ok(result));

    }
        //Либо юзер-сеты, либо вынести в отдельный контроллер и модели
        //NEW--------------------------------------------------------------------
        /*
        [HttpPatch]
        [AllowAnonymous]
        public async Task<IActionResult> AddToFavorites(Guid postId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Добавление поста в избранное по идентификатору");
            var result = _postService.AddToFavorites(postId, cancellationToken);
            return Ok(result);
        }

        [HttpPatch]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteFromFavorites(Guid postId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Добавление поста в избранное по идентификатору");
            var result = _postService.DeleteFromFavorites(postId, cancellationToken);
            return Ok();
        }
        */

    }

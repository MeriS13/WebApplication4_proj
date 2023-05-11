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
///</summary>

[ApiController]
[Route(template: "posts")]
//[AllowAnonymous]
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
    /// <response code="200"> Список объявлений успешно получен. </response>
    /// <response code="204"> Список объявлений пуст. </response>
    /// <returns> Список постов </returns>
    [HttpGet(template: "GetPosts/all")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос объявлений");
        var result = _postService.GetAll(cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    /// <summary>
    /// Получить объявление по идентификатору.
    /// </summary>
    /// <param name="postId">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="200"> Объявление успешно получено. </response>
    /// <response code="204"> Нет объявления с введенным идентификатором. </response>
    /// <returns>Модель объявления.</returns>
    [HttpGet("GetById{postId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetById(Guid postId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос получение объявления по идентификатору");
        var result = await _postService.GetByIdAsync(postId, cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    ///<summary>
    /// Сохраняет новое объявление 
    ///</summary>
    ///<param name="dto"> Модель создания объявления </param>
    ///<param name="cancellationToken">Токен отмены операции</param>
    ///<response code="401"> Пользователь не авторизован. </response>
    ///<response code="201"> Объявление успешно сохранено. </response>
    ///<returns> Модель созданного объявления </returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(message: $"Сохранение объявления {JsonConvert.SerializeObject(dto)}");
        var result = await _postService.CreatePostAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Обновить объявление.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="dto">Модель обновления объявления.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns> Модель обновлённого объявления. </returns>
    /// <response code="201"> Объявление успешно изменено. </response>
    /// <response code="404"> Нет объявления с введенным идентификатором. </response>
    /// <response code="403"> Нельзя редактировать объявление созданное другим пользователем. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpPut("Update/{id:Guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePostDto dto, CancellationToken cancellationToken)
    {
        var postdto = await _postService.GetByIdAsync(id, cancellationToken);

        if (postdto == null)
            return StatusCode((int)HttpStatusCode.NotFound);
        if (postdto.AccountId != _postService.GetCurrentUserId())
            return StatusCode((int)HttpStatusCode.Forbidden);

        var result = await _postService.UpdateAsync(id, dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Удаление объявления по параметру id
    /// </summary>
    /// <returns> StatusCode </returns>
    /// <response code="204"> Объявление успешно удалено. </response>
    /// <response code="404"> Нет объявления с введенным идентификатором. </response>
    /// <response code="403"> Нельзя удалить объявление созданное другим пользователем. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        var postdto = await _postService.GetByIdAsync(id, cancellationToken);

        if (postdto == null) 
            return StatusCode((int)HttpStatusCode.NotFound);
        if (postdto.AccountId != _postService.GetCurrentUserId())
            return StatusCode((int)HttpStatusCode.Forbidden);

        await _postService.DeleteById(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }



    /// <summary>
    /// Получить список постов по идентификатору категории.
    /// </summary>  где-то в сервисе преобразование не происходит
    /// <param name="CategoryId"> Идентификатор категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <response code="200"> Список объявлений </response>
    /// <response code="204"> Нет объявлений или неверный идентификатор категории. </response>
    /// <returns> Список постов </returns>
    [HttpGet("GetByCategoryId/{CategoryId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPostsByCategoryId(Guid CategoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка постов для категории");

        var result = _postService.GetAllPostsByCategoryId(CategoryId, cancellationToken);

        if (result == null) return StatusCode((int)HttpStatusCode.NoContent);
        return await Task.Run(() => Ok(result));
    }

    /// <summary>
    /// Получить список постов текущего пользователя
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns> Список постов </returns>
    /// <response code="200"> Список объявлений </response>
    /// <response code="204"> Нет объявлений или неверный идентификатор родительской категории. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpGet("GetPostByCurrentUser")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserPostsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка постов для текущего аккаунта");


        var result = _postService.GetUserPosts(cancellationToken);
        if (result == null) return StatusCode((int)HttpStatusCode.NoContent);

        return await Task.Run(() => Ok(result));
    }

    /// <summary>
    /// Получить список постов, относящихся к определенной родительской категории по ее Id
    /// </summary>
    /// <param name="ParCatId"> Идентификатор родительской категории </param>
    /// <param name="cancellationToken"> токен отмены операции </param>
    /// <returns> Список постов </returns>
    /// <response code="200"> Список объявлений </response>
    /// <response code="204"> Нет объявлений или неверный идентификатор родительской категории. </response>
    [HttpGet("GetPostsByParentCategoryId/{ParCatId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPostsByParentCategoryId(Guid ParCatId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка постов по Id родительской категории для категории, к которой относится пост");
        var result = _postService.GetAllPostsByParentCategoryId(ParCatId, cancellationToken);
        return await Task.Run(() => Ok(result));

    }

    }

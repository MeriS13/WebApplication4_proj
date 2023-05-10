using Board.Application.AppData.Contexts.Comments.Services;
using Board.Contracts.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

[ApiController]
[Route(template: "comments")]
[AllowAnonymous]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;
    private readonly ILogger<CommentsController> _logger;

    public CommentsController(ILogger<CommentsController> logger, ICommentsService commentsService)
    {
        _logger = logger;
        _commentsService = commentsService;
    }

    /// <summary>
    /// Добавление комментария к посту по идентификатору поста
    /// </summary>
    /// <param name="dto"> Модель комментария </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Идентификатор созданного коммента </returns>
    [HttpPost]
    public async Task<IActionResult> CreateByPostId([FromBody] CreateCommentDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Добавление комментария к посту по идентификатору поста.");
        var result = await _commentsService.CreateByPostIdAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
        //return Ok();
    }


    /// <summary>
    /// Удаление комментария по его идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор комментария </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Статус код</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteByCommentId(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление комментария по идентификатору.");
        await _commentsService.DeleteByCommentId(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }


    /// <summary>
    ///  Получение списка комментариев к посту по идентификатору поста.
    /// </summary>
    /// <param name="postId"> Идентификатор поста. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список комментов. </returns>
    [HttpGet("GetComments/{postId:Guid}")]
    public async Task<IActionResult> GetCommentsOnPostById(Guid postId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение списка комментариев к посту по идентификатору поста.");
        var result = await _commentsService.GetCommentsOnPostByIdAsync(postId, cancellationToken);
        return await Task.Run(() => Ok(result));
    }


    /// <summary>
    /// Получение комментариев текущего пользователя (по Id через клеймы).
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список комментариев. </returns>
    [HttpGet]
    public async Task<IActionResult> GetCommentsCurrentUser(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение комментариев текущего пользователя (по Id через клеймы).");
        var result = await _commentsService.GetCommentsCurrentUserByIdAsync(cancellationToken);
        return await Task.Run(() => Ok(result));
    }

}

using Board.Application.AppData.Contexts.Comments.Services;

using Board.Contracts.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

[ApiController]
[Route(template: "comments")]
//[AllowAnonymous]
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
    /// <response code="401"> Пользователь не авторизован. </response>
    /// <response code="201"> Ответ сохранён. </response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateByPostId([FromBody] CreateCommentDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Добавление комментария к посту по идентификатору поста.");

        var result = await _commentsService.CreateCommentAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }


    /// <summary>
    /// Удаление комментария по его идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор комментария </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Статус код</returns>
    /// <response code="204"> Комментарий успешно удалено. </response>
    /// <response code="404"> Нет комментария с введенным идентификатором. </response>
    /// <response code="403"> Нельзя удалить комментарий другого пользователя. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpDelete("{id:Guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteByCommentIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление комментария по идентификатору.");

        var existingcomment = await _commentsService.GetByIdAsync(id, cancellationToken);
        if(existingcomment == null) 
            return StatusCode((int)HttpStatusCode.NotFound);

        if (existingcomment.AccId != _commentsService.GetCurrentUserId() || _commentsService.GetCurrentUserName() != "Admin")
            return StatusCode((int)HttpStatusCode.Forbidden);

        await _commentsService.DeleteByCommentId(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }


    /// <summary>
    ///  Получение списка комментариев к посту по идентификатору поста.
    /// </summary>
    /// <param name="postId"> Идентификатор поста. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список комментов. </returns>
    /// <response code="200"> Список комментариев </response>
    /// <response code="204"> Нет комментариев или неверный идентификатор объявления. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpGet("GetComments/{postId:Guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCommentsOnPostById(Guid postId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение списка комментариев к объявлению по идентификатору объявления.");

        var result = await _commentsService.GetCommentsOnPostByIdAsync(postId, cancellationToken);
        if(result == null) return StatusCode((int)HttpStatusCode.NoContent);
        return await Task.Run(() => Ok(result));
    }


    /// <summary>
    /// Получение комментариев текущего пользователя.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список комментариев. </returns>
    /// <response code="200"> Список комментариев </response>
    /// <response code="204"> Нет комментариев. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCommentsCurrentUser(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение комментариев текущего пользователя.");

        var result = await _commentsService.GetCommentsCurrentUserByIdAsync(cancellationToken);
        if (result == null) 
            return StatusCode((int)HttpStatusCode.NoContent);

        return await Task.Run(() => Ok(result));
    }

    [HttpPost("CreateAnswer")]
    public async Task<IActionResult> CreateAnswer(CreateAnswerDto dto, CancellationToken cancellationToken)
    {
        var result = await _commentsService.CreateAnswer(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    [HttpGet("GetAnewers/{commentId:Guid}")]
    public async Task<IActionResult> GetAnswersByCommentId(Guid commentId, CancellationToken cancellationToken)
    {
        var result = await _commentsService.GetAnswersByCommentId(commentId, cancellationToken);
        return StatusCode((int)HttpStatusCode.OK, result);
    }

}

using Board.Application.AppData.Contexts.Answers.Services;
using Board.Application.AppData.Contexts.Comments.Services;
using Board.Contracts.Answers;
using Board.Contracts.Comments;
using Board.Contracts.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с ответами
/// </summary>


[ApiController]
[Route(template: "answer")]
//[AllowAnonymous]
public class AnswerController : ControllerBase
{
    private readonly IAnswerService _answerService;
    private readonly ILogger<AnswerController> _logger;

    public AnswerController(ILogger<AnswerController> logger, IAnswerService answerService)
    {
        _logger = logger;
        _answerService = answerService;
    }


    /// <summary>
    /// Создать ответ к комментарию.
    /// </summary>
    /// <param name="dto"> Модель создания ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Идентификаор созданного ответа. </returns>
    ///<response code="401"> Пользователь не авторизован. </response>
    ///<response code="201"> Ответ сохранён. </response>
    [Authorize]
    [HttpPost("CreateAnswer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateByCommentIdAsync([FromBody] CreateAnswerDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Добавление .");

        var result = await _answerService.CreateByCommentIdAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }


    /// <summary>
    /// Удаление ответа по его идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> - </returns>
    /// <response code="204"> Ответ успешно удалено. </response>
    /// <response code="404"> Нет ответа с введенным идентификатором. </response>
    /// <response code="403"> Нельзя удалить ответ другого пользователя. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete .");

        var answerdto = await _answerService.GetByIdAsync(id, cancellationToken);
        if (answerdto == null)
            return StatusCode((int)HttpStatusCode.NotFound);
        if (answerdto.AccId != _answerService.GetCurrentUserId() || _answerService.GetCurrentUserName() != "Admin")
            return StatusCode((int)HttpStatusCode.Forbidden);

        await _answerService.DeleteById(id, cancellationToken);

        return StatusCode((int)HttpStatusCode.NoContent, null);
    }


    /// <summary>
    /// Получение списка ответов к комментарию.
    /// </summary>
    /// <param name="commentId"> Идентификатор комментария. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список ответов. </returns>
    /// <response code="200"> Список ответов </response>
    /// <response code="204"> Нет ответов или неверный идентификатор комментария. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [Authorize]
    [HttpGet("GetAnswers/{commentId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAnswersOnCommentsById(Guid commentId, CancellationToken cancellationToken)
    {
        //_logger.LogInformation("Получение списка комментариев к посту по идентификатору поста.");
        var result = await _answerService.GetAnswersOnCommentsById(commentId, cancellationToken);
        if(result == null) return StatusCode((int)HttpStatusCode.NoContent);
        return await Task.Run(() => Ok(result));
    }

    // Подумать как получить список комментов со списками ответов к нему 

}

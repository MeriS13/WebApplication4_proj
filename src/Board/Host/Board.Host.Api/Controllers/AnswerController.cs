using Board.Application.AppData.Contexts.Answers.Services;
using Board.Application.AppData.Contexts.Comments.Services;
using Board.Contracts.Answers;
using Board.Contracts.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с ответами
/// </summary>


[ApiController]
[Route(template: "answer-controller")]
[AllowAnonymous]
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
    /// Создать ответ к комменту по идентификаору коммента.
    /// </summary>
    /// <param name="dto"> Модель создания ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Идентификаор созданного ответа. </returns>
    [Authorize]
    [HttpPost("CreateByCommentId")]
    public async Task<IActionResult> CreateByCommentIdAsync([FromBody] CreateAnswerDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Добавление .");

        var result = await _answerService.CreateByCommentIdAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
        //return Ok();
    }


    /// <summary>
    /// Удаление ответа по его идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор ответа. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> - </returns>
    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Добавление .");

        await _answerService.DeleteById(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }


    /// <summary>
    /// Получение списка ответов к комментарию.
    /// </summary>
    /// <param name="commentId"> Идентификатор комментария. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список ответов. </returns>
    [Authorize]
    [HttpGet("GetAnswersOnCommentsById")]
    public async Task<IActionResult> GetAnswersOnCommentsById(Guid commentId, CancellationToken cancellationToken)
    {
        //_logger.LogInformation("Получение списка комментариев к посту по идентификатору поста.");
        var result = await _answerService.GetAnswersOnCommentsById(commentId, cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    /* Подумать как получить список комментов со списками ответов к нему 
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllMessageByPostId(Guid postId, CancellationToken cancellationToken)
    {

        return await Task.Run(() => Ok(result));
    }
    */
}

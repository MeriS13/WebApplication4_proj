using Board.Application.AppData.Contexts.Categories.Services;
using Board.Application.AppData.Contexts.Comments.Services;
using Board.Contracts.Category;
using Board.Contracts.Comments;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

[ApiController]
[Route(template: "comments-controller")]
public class CommentsController : ControllerBase
{
    private readonly ICommentsService _commentsService;
    private readonly ILogger<CategoryController> _logger;

    public CommentsController(ILogger<CategoryController> logger, ICommentsService commentsService)
    {
        _logger = logger;
        _commentsService = commentsService;
    }

    /// <summary>
    /// Добавление комментария к посту по идентификатору поста
    /// </summary>
    /// <param name="dto"> Модель комментария </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateByPostId([FromBody] CommentDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Добавление комментария к посту по идентификатору поста.");
        //var result = await _commentsService.CreateAsync(dto, cancellationToken);
        //return StatusCode((int)HttpStatusCode.Created, result);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteByCommentId(Guid id, CancellationToken cancellationToken)
    {

    }

    [HttpGet]
    public async Task<IActionResult> GetCommentsOnPostById(Guid postId, CancellationToken cancellationToken)
    {

    }

    [HttpGet]
    public async Task<IActionResult> GetCommentsCarrentUserById(Guid userId, CancellationToken cancellationToken)
    {

    }

    // gjkexbnm jndtns r rjvvtyne 
}

using Board.Application.AppData.Contexts.Categories.Services;
using Board.Application.AppData.Contexts.ParentCategories.Services;
using Board.Contracts.Category;
using Board.Contracts.ParentCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с родительскими категориями
/// </summary>

[ApiController]
[Route(template: "parent_categories")]
public class ParentCategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly IParentCategoryService _parent_categoryService;

    public ParentCategoryController(IParentCategoryService parent_categoryService, ILogger<CategoryController> logger)
    {
        _parent_categoryService = parent_categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список родительских категорий
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список родительских категорий</returns>
    [HttpGet("GetAll")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка категорий.");
        var result = await _parent_categoryService.GetAllAsync(cancellationToken);
        return await Task.Run(() => Ok(result));
    }


    /// <summary>
    /// Создать новую категорию.
    /// </summary>
    /// <param name="dto"> Модель создания родительской категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Идентификатор созданной категории </returns>
    [HttpPost()]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateParentCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание новой категории.");
        var result = await _parent_categoryService.CreateAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор родительской категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns></returns>
    [HttpGet("GetCategory/{categoryid:Guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос категории по идентификатору.");
        var result = await _parent_categoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор род.категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Результат выполнения </returns>
    [HttpDelete]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление категории по идентификатору.");
        await _parent_categoryService.DeleteById(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }
}

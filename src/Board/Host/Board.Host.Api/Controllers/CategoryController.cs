using Board.Application.AppData.Contexts.Posts.Services;
using Microsoft.AspNetCore.Mvc;
using Board.Contracts.Category;
using Board.Application.AppData.Contexts.Categories.Services;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с категориями
/// </summary>

[ApiController]
[Route(template: "categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;


    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    /// Получить список категорий.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос категорий.");
        var result = _categoryService.GetAllAsync(cancellationToken);
        return await Task.Run(() => Ok(result));
    }

    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    [HttpGet("GetById/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос категории по идентификатору.");
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создать новую категорию.
    /// </summary>
    [HttpPost("Create")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание новой категории.");
        var result = await _categoryService.CreateAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Обновить категорию.
    /// </summary>
    [HttpPut("{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обновление существующей категории.");
        var result = await _categoryService.UpdateAsync(id, dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result); 
    }

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление категории по идентификатору.");
        await _categoryService.DeleteByIdAsync(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }

    /// <summary>
    /// Получить список категорий, относящихся к заданной родительской категории.
    /// </summary>
    /// <param name="id"> Идентификатор родительской категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Список категорий </returns>
    [HttpGet("GetCategories/{parentId:Guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCategoriesByParentIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос родительской категории по идентификатору.");
        var result = await _categoryService.GetCategoriesByParentIdAsync(id, cancellationToken);
        return Ok(result);
    }

}

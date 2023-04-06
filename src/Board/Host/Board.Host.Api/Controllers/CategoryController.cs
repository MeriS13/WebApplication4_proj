using Board.Application.AppData.Contexts.Posts.Services;
using Microsoft.AspNetCore.Mvc;
using Board.Contracts.Category;
using Board.Application.AppData.Contexts.Categories.Services;
using System.Net;

namespace Board.Host.Api.Controllers;


[ApiController]
[Route(template: "categories-controller")]
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
    
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос категорий");

        return await Task.Run(() => Ok(Enumerable.Empty<CategoryDto>()), cancellationToken);
    }

    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    
    [HttpGet("lalala")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создать новую категорию.
    /// </summary>
   
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.CreateAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Обновить категорию.
    /// </summary>
    
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result); 
    }

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    [HttpDelete("lalala2")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteByIdAsync(id, cancellationToken);
        //return await Task.Run(NoContent, cancellationToken); //?status code
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }

    [HttpGet("{CategoryId:Guid}")]
    public async Task<IActionResult> GetAllPosts(Guid CategoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка постов для категории");

        return await Task.Run(() => Ok(Enumerable.Empty<CategoryDto>()), cancellationToken);//!!!!//?status code
    }
}

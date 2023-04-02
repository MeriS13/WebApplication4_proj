using Board.Application.AppData.Contexts.Posts.Services;
using Microsoft.AspNetCore.Mvc;
using Board.Contracts.Category;
using Board.Application.AppData.Contexts.Categories;

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

        return await Task.Run(() => Ok(Enumerable.Empty<CreateCategoryDto>()), cancellationToken);
    }

    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await Task.Run(() => Ok(new CreateCategoryDto()), cancellationToken);
    }

    /// <summary>
    /// Создать новую категорию.
    /// </summary>
   
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        return await Task.Run(() => CreatedAtAction(nameof(GetById), new { Guid.Empty }), cancellationToken);
    }

    /// <summary>
    /// Обновить категорию.
    /// </summary>
    
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        return await Task.Run(() => Ok(new CreateCategoryDto()), cancellationToken);
    }

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        return await Task.Run(NoContent, cancellationToken);
    }
}

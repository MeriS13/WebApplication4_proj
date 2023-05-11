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
    /// <response code="200"> Список категорий </response>
    /// <response code="204"> Нет категорий. </response> 
    [HttpGet("GetAll")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка родительских категорий.");

        var result = await _parent_categoryService.GetAllAsync(cancellationToken);
        if (result == null) return StatusCode((int)HttpStatusCode.NoContent);

        return await Task.Run(() => Ok(result));
    }


    /// <summary>
    /// Создать новую категорию.
    /// </summary>
    /// <param name="dto"> Модель создания родительской категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Идентификатор созданной категории </returns>
    ///// <response code="400"> Категория с таким названием уже существует. </response>
    /// <response code="201"> категория успешно создана. </response>
    /// <response code="403"> Доступно для редактирования только пользователю Admin </response>
    /// <response code="401"> Пользователь не авторизован. </response> 
    [HttpPost()]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateParentCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание новой родительской категории.");

        var result = await _parent_categoryService.CreateAsync(dto, cancellationToken);

        if (result == null) return StatusCode((int)HttpStatusCode.NoContent);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор родительской категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> Модель категории. </returns>
    /// <response code="200"> Модель категории успешно получена. </response>
    /// <response code="204"> Неверный идентификатор. </response>
    [HttpGet("GetCategory/{id:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос родительской категории по идентификатору.");

        var result = await _parent_categoryService.GetByIdAsync(id, cancellationToken);
        if (result == null) return StatusCode((int)HttpStatusCode.NoContent);
        return Ok(result);
    }

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор род.категории </param>
    /// <param name="cancellationToken"> Токен отмены операции </param>
    /// <returns> - </returns>
    /// <response code="204"> Успешно удалено. </response>
    /// <response code="403"> Доступно для редактирования только пользователю Admin </response>
    /// <response code="401"> Пользователь не авторизован. </response> 
    /// <response code="404"> Нет категории с таким идентификатором. </response>
    [HttpDelete]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление родительской категории по идентификатору.");

        var existingCategory = await _parent_categoryService.GetByIdAsync(id, cancellationToken);
        if (existingCategory == null) return StatusCode((int)HttpStatusCode.NotFound);

        await _parent_categoryService.DeleteById(id, cancellationToken);

        return StatusCode((int)HttpStatusCode.NoContent, null);
    }
}

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
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Список категорий. </returns>
    /// <response code="200"> Список категорий </response>
    /// <response code="204"> Нет категорий. </response> 
    [AllowAnonymous]
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос категорий.");

        var result = await _categoryService.GetAllAsync(cancellationToken);

        if(result == null) return StatusCode((int)HttpStatusCode.NoContent);

        return await Task.Run(() => Ok(result));
    }


    /// <summary>
    /// Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор категории. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Модель категории. </returns>
    /// <response code="200"> Модель категории успешно получена. </response>
    /// <response code="204"> Неверный идентификатор. </response>
    [HttpGet("GetById/{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос категории по идентификатору.");
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);

        if (result == null) return StatusCode((int)HttpStatusCode.NoContent);

        return Ok(result);
    }

    /// <summary>
    /// Создать новую категорию.
    /// </summary>
    /// <param name="dto"> Модель создания категории. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Идентификатор созданной категории. </returns>
    /// <response code="201"> категория успешно создана. </response>
    /// <response code="400"> Категория с таким названием уже существует. </response>
    /// <response code="403"> Доступно для редактирования только пользователю Admin </response>
    /// <response code="401"> Пользователь не авторизован. </response> 
    [HttpPost("Create")]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание новой категории.");

        var result = await _categoryService.CreateAsync(dto, cancellationToken);

        if(result == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            return StatusCode((int)HttpStatusCode.BadRequest);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Обновить категорию.
    /// </summary>
    /// <param name="id"> Идентификатор категории. </param>
    /// <param name="dto"> Модель обновления категории. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> Модель категории. </returns>
    /// <response code="200"> Категория успешно обновлена. </response>
    /// <response code="403"> Доступно для редактирования только пользователю Admin </response>
    /// <response code="401"> Пользователь не авторизован. </response> 
    /// <response code="204"> Нет категории или неверный идентификатор. </response>
    [HttpPut("{id:Guid}")]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateAsync (Guid id, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обновление существующей категории.");
        var result = await _categoryService.UpdateAsync(id, dto, cancellationToken);

        if (result == null) return StatusCode((int)HttpStatusCode.NoContent);

        return StatusCode((int)HttpStatusCode.OK, result); 
    }

    /// <summary>
    /// Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id"> Идентификатор категории. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> StatusCode </returns>
    /// <response code="404"> Нет категории с таким идентификатором. </response>
    /// <response code="403"> Доступно для редактирования только пользователю Admin </response>
    /// <response code="401"> Пользователь не авторизован. </response> 
    /// <response code="204"> Успешно удалено. </response> 
    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление категории по идентификатору.");

        var existingCategory = await _categoryService.GetByIdAsync(id, cancellationToken);
        if (existingCategory == null) return StatusCode((int)HttpStatusCode.NotFound);

        await _categoryService.DeleteByIdAsync(id, cancellationToken);
        return StatusCode((int)HttpStatusCode.NoContent, null);
    }


    /// <summary>
    /// Создание родительской категории.
    /// </summary>
    /// <param name="dto"> Доменная модель родительской категории. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> StatusCode </returns>
    /// <response code="201"> Родительская категория успешно создана. </response>
    /// <response code="403"> Доступно для редактирования только пользователю Admin </response>
    /// <response code="401"> Пользователь не авторизован. </response> 
    [HttpPost("CreateParCat")]
    [Authorize(Policy = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateParCatAsync(CreateParCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на создание родительской категории.");
        var result = await _categoryService.CreateParCatAsync(dto, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Получение списка родительских категорий.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> StatusCode </returns>
    /// <response code="200"> Категория успешно обновлена. </response>
    [HttpGet("GetParcat")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetParCatAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на получение списка родительских категорий.");
        var result = await _categoryService.GetParCatAsync(cancellationToken);
        //if (result == null) return StatusCode((int)HttpStatusCode.NotFound);

        return StatusCode((int)HttpStatusCode.OK, result);
    }

    /// <summary>
    /// Получение списка категорий, принадлежащих к одной родительской категории.
    /// </summary>
    /// <param name="id"> Идентификтаор род.категории. </param>
    /// <param name="cancellationToken"> Токен отмены операции. </param>
    /// <returns> StatusCode </returns>
    /// <response code="200"> Категория успешно обновлена. </response>
    /// <response code="404"> Нет категории с таким идентификатором. </response>
    [HttpGet("GetParCatById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoriesByParentIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка категорий, относящихся к одной родительской категории.");

        var result = await _categoryService.GetCategoriesByParentIdAsync(id, cancellationToken);
        if (result == null) return StatusCode((int)HttpStatusCode.NotFound);

        return StatusCode((int)HttpStatusCode.OK, result);
    }
}

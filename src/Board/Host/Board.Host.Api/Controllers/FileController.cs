using Board.Application.AppData.Contexts.Files.Services;
using Board.Contracts;
using Board.Contracts.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с файлами.
/// </summary>

 
[ApiController]
[Route(template: "files")]
[Produces("application/json")]
//[AllowAnonymous]

public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private readonly IFileService _fileService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="FileController"/>
    /// </summary>
    /// <param name="fileService">Сервис работы с файлами.</param>
    /// <param name="logger">Сервис логирования.</param>
    public FileController(IFileService fileService, ILogger<FileController> logger)
    {
        _logger = logger;
        _fileService = fileService;
    }

    /// <summary>
    /// Получение информации о файле по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Информация о файле.</returns>
    /// <response code="404"> Нет файла с введенным идентификатором. </response>
    /// <response code="200"> Информация успешно получена. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpGet("{id}/info")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetInfoById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение информации о файле по идентификатору.");
        var result = await _fileService.GetInfoByIdAsync(id, cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }


    /// <summary>
    /// Получение списка файлов, относящихся к объявлению по его Id.
    /// </summary>
    /// <param name="postId"> Идентификатор объявления.</param>
    /// <param name="cancellationToken"> Токен отмены. </param>
    /// <returns> Информация о файле. </returns>
    /// <response code="404"> Нет файлов или неверный идентификатор. </response>
    /// <response code="200"> Информация успешно получена. </response>
    [HttpGet("GetAll/{postId:Guid}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByPostIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение списка файлов, относящихся к объявлению по его Id.");
        var result = await _fileService.GetAllByPostIdAsync(postId, cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }



    /// <summary>
    /// Загрузка файла в систему.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <param name="postId"> Идентификатор объявления. </param>
    /// <param name="cancellationToken">Токен отмены.</param>
    ///// <response code="403"> Нельзя удалить комментарий другого пользователя. </response>
    /// <response code="404"> Нет файлов или неверный идентификатор. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    /// <response code="201"> Файл успешно загружен. </response>
    [HttpPost]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    [DisableRequestSizeLimit]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Upload(IFormFile file, Guid postId,  CancellationToken cancellationToken)
    {
        _logger.LogInformation("Загрузка файла в систему.");
        var bytes = await GetBytesAsync(file, cancellationToken);
        var fileDto = new FileDto
        {
            Content = bytes,
            ContentType = file.ContentType,
            Name = file.FileName
            
        };
        var result = await _fileService.UploadAsync(fileDto, postId, cancellationToken);

        if(result == Guid.Parse("00000000-0000-0000-0000-000000000000")) return NotFound();

        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Скачивание файла по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Файл в виде потока.</returns>
    /// <response code="404"> Нет файла с указанным идентификатором. </response>
    /// <response code="200"> Файл готов к скачиванию. </response>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Скачивание файла по идентификатору.");

        var result = await _fileService.DownloadAsync(id, cancellationToken);

        if (result == null)
            return NotFound();
        

        Response.ContentLength = result.Content.Length;
        return File(result.Content, result.ContentType, result.Name, true);
    }


    /// <summary>
    /// Удаление файла по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="204"> Файл успешно удалено. </response>
    /// <response code="404"> Нет файла с введенным идентификатором. </response>
    /// <response code="403"> Нельзя удалить файл другого пользователя. </response>
    /// <response code="401"> Пользователь не авторизован. </response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление файла по идентификатору.");

        var existingfile = await _fileService.GetInfoByIdAsync(id, cancellationToken);
        if (existingfile == null) 
            return NotFound();

        if(existingfile.AccountId != _fileService.GetCurrentUserId() 
            || _fileService.GetCurrentUserName() != "Admin")
            return StatusCode((int)HttpStatusCode.Forbidden);

        await _fileService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }



    private static async Task<byte[]> GetBytesAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms, cancellationToken);
        return ms.ToArray();
    }
}
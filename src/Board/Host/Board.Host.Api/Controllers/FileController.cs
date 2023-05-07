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
/// <response code="500">Произошла внутренняя ошибка.</response>
[ApiController]
[Route(template: "files-controller")]
[Produces("application/json")]
[AllowAnonymous]
//[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
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
    [HttpGet("{id}/info")]
    public async Task<IActionResult> GetInfoById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _fileService.GetInfoByIdAsync(id, cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }


    /// <summary>
    /// Получение информации о файле по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Информация о файле.</returns>
    [HttpGet("/info-by/{postId}")]
    public async Task<IActionResult> GetAllByPostIdAsync(Guid postId, CancellationToken cancellationToken)
    {
        var result = await _fileService.GetAllByPostIdAsync(postId, cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }



    /// <summary>
    /// Загрузка файла в систему.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpPost]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> Upload(IFormFile file, Guid postId,  CancellationToken cancellationToken)
    {
        var bytes = await GetBytesAsync(file, cancellationToken);
        var fileDto = new FileDto
        {
            Content = bytes,
            ContentType = file.ContentType,
            Name = file.FileName
            
        };
        var result = await _fileService.UploadAsync(fileDto, postId, cancellationToken);
        return StatusCode((int)HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Скачивание файла по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Файл в виде потока.</returns>
    [HttpGet("{id}")]
    //[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
    {
        var result = await _fileService.DownloadAsync(id, cancellationToken);

        if (result == null)
        {
            return NotFound();
        }

        Response.ContentLength = result.Content.Length;
        return File(result.Content, result.ContentType, result.Name, true);
    }


    /// <summary>
    /// Удаление файла по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
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
using Board.Contracts;
using Board.Contracts.File;
using Microsoft.AspNetCore.Mvc;

namespace Board.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с файлами.
/// </summary>
[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;

    /// <summary>
    /// Инициализирует экземпляр <see cref="FileController"/>
    /// </summary>
    /// <param name="logger">Сервис логирования.</param>
    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Получение информации о файле по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Информация о файле.</returns>
    [HttpGet("{id}/info")]
    public async Task<IActionResult> GetInfoById(string id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Ok(new FileInfoDto()));
    }

    /// <summary>
    /// Загрузка файла в систему.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <param name="cancellationToken">Токен отмены.</param>

    [HttpPost]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        return await Task.Run(() => CreatedAtAction(nameof(Download), new { string.Empty }), cancellationToken);
    }


    /// <summary>
    /// Скачивание файла по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Файл в виде потока.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Download(string id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(File(new MemoryStream(), string.Empty, string.Empty));
    }


    /// <summary>
    /// Удаление файла по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор файла.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
    {
        return await Task.FromResult(NoContent());
    }
}
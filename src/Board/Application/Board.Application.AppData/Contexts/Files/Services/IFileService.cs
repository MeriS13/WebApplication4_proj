using Board.Contracts.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Files.Services
{
    /// <summary>
    /// Сервис по работе с файлами.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Метод для получения Id текущего пользователя из контекста
        /// </summary>
        /// <returns> Id текущего пользователя </returns>
        Guid GetCurrentUserId();

        /// <summary>
        /// Метод для получения имени текущего пользователя из контекста
        /// </summary>
        /// <returns> Имя текущего пользователя </returns>
        string GetCurrentUserName();


        /// <summary>
        /// Получение информации о файле по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о файле.</returns>
        Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="model"> Модель файла. </param>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <param name="postId"> Идентификатор объявления, к которому относится файл </param>
        /// <returns> Идентификатор загруженного файла. </returns>
        Task<Guid> UploadAsync(FileDto model, Guid postId, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о скачиваемом файле.</returns>
        Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удаление файла по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>        
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получение списка моделей информации о файлах, принадлежащих объявлению по его идентификатору
        /// </summary>
        /// <param name="postId"> Идентификатор объявления </param>
        /// <param name="cancellationToken"> Токен отмены операции</param>
        /// <returns></returns>
        Task<List<FileInfoDto>> GetAllByPostIdAsync(Guid postId, CancellationToken cancellationToken);
    }
}

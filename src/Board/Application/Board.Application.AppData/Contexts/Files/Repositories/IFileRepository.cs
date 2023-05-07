using Board.Contracts.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Files.Repositories
{
    public interface IFileRepository
    {
        /// <summary>
        /// Получение информации о файле по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о файле.</returns>
        Task<Board.Domain.Files.File> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Загрузка файла в систему.
        /// </summary>
        /// <param name="model">Модель файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Идентификатор загруженного файла.</returns>
        Task<Guid> UploadAsync(Domain.Files.File model, CancellationToken cancellationToken);

        /// <summary>
        /// Скачивание файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Информация о скачиваемом файле.</returns>
        Task<Board.Domain.Files.File> DownloadAsync(Guid id, CancellationToken cancellationToken);

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
        IQueryable<Domain.Files.File> GetAllByPostId(Guid postId, CancellationToken cancellationToken);
    }
}

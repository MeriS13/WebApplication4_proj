using Board.Application.AppData.Contexts.Files.Repositories;
using Board.Contracts.File;
using Board.Domain.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Files.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        /// <summary>
        /// Инициализация экземпляра <see cref="FileService"/>.
        /// </summary>        
        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }



        /// <inheritdoc/>
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _fileRepository.DeleteAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<FileDto> DownloadAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _fileRepository.DownloadAsync(id, cancellationToken);

            var dto = new FileDto
            {
                Content = model.Content,
                ContentType = model.ContentType,
                Name = model.Name
            };

            return dto;
        }

        /// <inheritdoc/>
        public async Task<FileInfoDto> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var model = await _fileRepository.GetInfoByIdAsync(id, cancellationToken);
            if(model==null) 
            { 
                throw new Exception("Введеный идентификатор не соответствует ни одному файлу!"); 
            }
            var dto = new FileInfoDto
            {
                Id = model.Id,
                Length = model.Length,
                ContentType = model.ContentType,
                Name = model.Name
            };

            return dto;
        }

        /// <inheritdoc/>
        public Task<Guid> UploadAsync(FileDto dto, CancellationToken cancellationToken)
        {

            var file = new Board.Domain.Files.File
            {
                Content = dto.Content,
                ContentType = dto.ContentType,
                Created = DateTime.UtcNow,
                Name = dto.Name,
                Length = dto.Content.Length
            };
            return _fileRepository.UploadAsync(file, cancellationToken);
        }
    }
}

using Board.Application.AppData.Contexts.Files.Repositories;
using Board.Application.AppData.Contexts.Posts.Repositories;
using Board.Contracts.File;
using Board.Contracts.Posts;
using Board.Domain.Files;
using Board.Domain.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Board.Application.AppData.Contexts.Files.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Инициализация экземпляра <see cref="FileService"/>.
        /// </summary>        
        public FileService(IFileRepository fileRepository, IHttpContextAccessor httpContextAccessor, IPostRepository postRepository)
        {
            _fileRepository = fileRepository;
            _httpContextAccessor = httpContextAccessor;
            _postRepository = postRepository;
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
                Name = model.Name,
                AccountId = model.AccountId, //?
                PostId = model.PostId,      //?
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
                Name = model.Name,
                PostId = model.PostId,
                AccountId=model.AccountId,
            };

            return dto;
        }

        public IPostRepository Get_postRepository()
        {
            return _postRepository;
        }

        /// <inheritdoc/>
        public async Task<Guid> UploadAsync(FileDto dto, Guid postId, CancellationToken cancellationToken)
        {
            //получение идентификатора и имени пользователя из контекста 
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(claimId)) throw new Exception("ClaimId is null!");
            var UserId = Guid.Parse(claimId);

            //

            //нужно чтобы человек мог загрузить файл только к тому посту, который ему принадлежит 

            Post postdto = await _postRepository.GetByIdAsync(postId, cancellationToken);
            if (postdto == null) { throw new Exception("Не существует объявления с введенным идентификатором"); };

            var accId = postdto.AccountId;

            if (accId != UserId) throw new Exception("Текущий пользователь не может редактировать пост другого пользователя.");

            var file = new Board.Domain.Files.File
            {
                Content = dto.Content,
                ContentType = dto.ContentType,
                Name = dto.Name,
                Length = dto.Content.Length,
                Created = DateTime.UtcNow,
                PostId = postId,
                AccountId = UserId,
            };
            return await _fileRepository.UploadAsync(file, cancellationToken);
        }


        /// <inheritdoc/>
        public async Task<List<FileInfoDto>> GetAllByPostIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            List<Domain.Files.File> models =  _fileRepository.GetAllByPostId(postId, cancellationToken).ToList();
            if (models.IsNullOrEmpty())
            {
                throw new Exception("Введеный идентификатор не соответствует ни одному файлу!");
            }
            List<FileInfoDto> dtolist = new();
            foreach(var model in models)
            {
                dtolist.Add(new FileInfoDto
                {
                    Id = model.Id,
                    Length = model.Length,
                    ContentType = model.ContentType,
                    Name = model.Name,
                    PostId = model.PostId,
                    AccountId = model.AccountId,
                });
            }

            return dtolist;
        }
    }
}

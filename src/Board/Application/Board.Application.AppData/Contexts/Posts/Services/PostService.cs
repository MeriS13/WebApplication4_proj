using Board.Application.AppData.Contexts.Accounts.Repositories;
using Board.Application.AppData.Contexts.Categories.Repositories;
using Board.Application.AppData.Contexts.Posts.Repositories;
using Board.Contracts.Comments;
using Board.Contracts.Posts;
using Board.Domain.Categories;
using Board.Domain.Comments;
using Board.Domain.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


//Остановилась тут. Надо написать маппинг для моделек и смотреть репозиторий . потом коментами рд

namespace Board.Application.AppData.Contexts.Posts.Services;

/// <inheritdoc cref="IPostService"/>
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="postService"> интерфейс сервиса </param>
    public PostService(IPostRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _postRepository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public Task<Guid> CreatePostAsync(CreatePostDto dto, CancellationToken cancellationToken)
    {
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) return null;

        var UserId = Guid.Parse(claimId);
        //end

        var entity = new Post
        {
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            IsFavorite = dto.IsFavorite,
            CreationDate = DateTime.UtcNow,
            AccountId = UserId,
        };
        return _postRepository.AddPostAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        return _postRepository.DeleteById(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<PostDto>> GetAll(CancellationToken cancellationToken)
    {
        //Получаем список доменных моделей, создаем список dto-моделей, в цикле добавляем
        //элементы списка - смаппленные модели к dto и возвращаем 
        List<Post> entities = _postRepository.GetAll(cancellationToken).ToList();
        List<PostDto> result = new();
        foreach (var entity in entities)
        {
            result.Add(new PostDto
            {
                Name = entity.Name,
                Id = entity.Id,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                IsFavorite = entity.IsFavorite,
                CreationDate = entity.CreationDate,
            });
            
        }
        return result;
    }

    /// <inheritdoc/>
    public async Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _postRepository.GetByIdAsync(id, cancellationToken);
        var result = new PostDto
        {
            Name = entity.Name,
            Id = entity.Id,
            Description = entity.Description,
            CategoryId = entity.CategoryId,
            IsFavorite = entity.IsFavorite,
            CreationDate = entity.CreationDate,
        };
        return result;
    }

    /*/// <inheritdoc/>
    public async Task<List<CommentDto>> GetAllCommentsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        //получаем список домен.моделек комментов
        List<Comment> entities = _postRepository.GetCommentsByIdAsync(id, cancellationToken).ToList();
        List<CommentDto> result = new();
        int i = 0;
        foreach (var entity in entities)
        {
            result.Add(new CommentDto
            {
                Id = entity.Id,
                CreationDate = entity.CreationDate,
                PostId = entity.PostId,
                Content = entity.Content,
                UserName = entity.UserName,
            });
        }
        return result;
    }*/

    /// <inheritdoc/> все ок
    public async Task<PostDto> UpdateAsync(Guid id, UpdatePostDto dto, CancellationToken cancellationToken)
    {
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) return null;

        var UserId = Guid.Parse(claimId);
        //end

        //Преобразуем модель обновления к доменной
        var entity = new Post
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            IsFavorite = dto.IsFavorite,
            CreationDate = dto.CreationDate,
            CategoryId = dto.CategoryId,
            AccountId = UserId,
        };
        //отдаем обновленную доменную в репозиторий, там она обновляется в бд, возвращается она же
        var newModel = await _postRepository.UpdateAsync(id, entity, cancellationToken);
        //преобразуем обновленную доменную к модели категории
        var newDto = new PostDto
        {
            Id = newModel.Id,
            Name = newModel.Name,
            Description = newModel.Description,
            IsFavorite = newModel.IsFavorite,
            CreationDate = newModel.CreationDate,
            CategoryId = newModel.CategoryId,
        };
        return newDto;
    }

    /// <inheritdoc/> все ок
    public async Task<List<PostDto>> GetAllPostsByCategoryId(Guid CategoryId, CancellationToken cancellationToken)
    {
        //получаем список домен.моделек постов
        List<Post> entities = _postRepository.GetAllPostsByCategoryId(CategoryId, cancellationToken).ToList();
        List<PostDto> result = new();
        foreach (var entity in entities)
        {
            result.Add(new PostDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreationDate = entity.CreationDate,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                IsFavorite = entity.IsFavorite,
            });
        }
        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PostDto>> GetUserPosts(CancellationToken cancellationToken)
    {
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) return null;

        var UserId = Guid.Parse(claimId);
        //end

        List<Post> entities = _postRepository.GetUserPosts(UserId, cancellationToken).ToList();
        List<PostDto> result = new();
        foreach (var entity in entities)
        {
            result.Add(new PostDto
            {
                Id = entity.Id,
                Name = entity.Name,
                CreationDate = entity.CreationDate,
                Description = entity.Description,
                CategoryId = entity.CategoryId,
                IsFavorite = entity.IsFavorite,
            });
        }
        return result;
    }
}


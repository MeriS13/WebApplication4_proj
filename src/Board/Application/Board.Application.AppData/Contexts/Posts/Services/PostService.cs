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
using System.Data;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;



namespace Board.Application.AppData.Contexts.Posts.Services;

/// <inheritdoc cref="IPostService"/>
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostService(IPostRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _postRepository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    // Метод для получения идентификатора текущего авторизованного пользователя из контекста
    public Guid GetCurrentUserId()
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return  Guid.Parse(claimId);
    }
    // Метод для получения имени текущего авторизованного пользователя из контекста
    public string GetCurrentUserName()
    {
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        return claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }
    

    /// <inheritdoc/>
    public async Task<Guid> CreatePostAsync(CreatePostDto dto, CancellationToken cancellationToken)
    {

        var entity = new Post
        {
            Name = dto.Name,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            IsFavorite = dto.IsFavorite,
            CreationDate = DateTime.UtcNow,
            AccountId = GetCurrentUserId()
        };
        return await _postRepository.AddPostAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await _postRepository.DeleteById(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<List<PostDto>> GetAll(CancellationToken cancellationToken)
    {
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
                AccountId = entity.AccountId,
            });
            
        }
        return result;
    }

    /// <inheritdoc/>
    public async Task<PostDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _postRepository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var result = new PostDto
        {
            Name = entity.Name,
            Id = entity.Id,
            Description = entity.Description,
            CategoryId = entity.CategoryId,
            IsFavorite = entity.IsFavorite,
            CreationDate = entity.CreationDate,
            AccountId = entity.AccountId,
        };
        return result;
    }

  

    /// <inheritdoc/> все ок
    public async Task<PostDto> UpdateAsync(Guid id, UpdatePostDto dto, CancellationToken cancellationToken)
    {
        
        //Обработка исключений
        var post = await _postRepository.GetByIdAsync(id, cancellationToken);

        //Преобразуем dto-модель обновления к доменной
        var entity = new Post
        {
            Id = post.Id,
            Name = dto.Name,
            Description = dto.Description,
            IsFavorite = dto.IsFavorite,
            CreationDate = DateTime.UtcNow,
            CategoryId = dto.CategoryId,
            AccountId = post.AccountId,
        };

        var newModel = await _postRepository.UpdateAsync(id, entity, cancellationToken);

        //преобразуем обновленную доменную к модели dto
        var newDto = new PostDto
        {
            Id = newModel.Id,
            Name = newModel.Name,
            Description = newModel.Description,
            IsFavorite = newModel.IsFavorite,
            CreationDate = newModel.CreationDate,
            CategoryId = newModel.CategoryId,
            AccountId=newModel.AccountId,
        };
        return newDto;
    }

    /// <inheritdoc/> норм
    public async Task<List<PostDto>> GetAllPostsByCategoryId(Guid CategoryId, CancellationToken cancellationToken)
    {
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
                AccountId = entity.AccountId,
            });
        }
        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PostDto>> GetUserPosts(CancellationToken cancellationToken)
    {

        List<Post> entities = _postRepository.GetUserPosts(GetCurrentUserId(), cancellationToken).ToList();
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
                AccountId = entity.AccountId,
            });
        }
        return result;
    }

    /// <inheritdoc/>
    
}


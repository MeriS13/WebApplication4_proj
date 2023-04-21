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

    /// <inheritdoc/>
    public async Task<Guid> CreatePostAsync(CreatePostDto dto, CancellationToken cancellationToken)
    {
        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) throw new Exception ("Функция недоступна.");

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
        return await _postRepository.AddPostAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public  Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        // Вся логика и обработка исключений в репозитории, т.к. тут оно не работает
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
            throw new Exception("Введеный идентификатор не принадлежит ни одному существующему объявлению!");
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
        //обработка исключения (проверка наличия пользователя в бд)
        var existingPost = await _postRepository.GetByIdAsync(id, cancellationToken);
        if (existingPost == null)
        {
            throw new Exception("Введеный идентификатор не принадлежит ни одному существующему объявлению!");
        }

        //получение идентификатора пользователя из контекста 
        var claims = _httpContextAccessor.HttpContext.User.Claims;
        var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(claimId)) return null;

        var UserId = Guid.Parse(claimId);
        //end

        //Преобразуем модель обновления к доменной
        var entity = new Post
        {
            Id = existingPost.Id,
            Name = dto.Name,
            Description = dto.Description,
            IsFavorite = dto.IsFavorite,
            CreationDate = DateTime.UtcNow,
            CategoryId = dto.CategoryId,
            AccountId = existingPost.AccountId,
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
            AccountId=newModel.AccountId,
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
                AccountId = entity.AccountId,
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
                AccountId = entity.AccountId,
            });
        }
        return result;
    }

    /// <inheritdoc/>
    public async Task<List<PostDto>> GetAllPostsByParentCategoryId(Guid ParCatId, CancellationToken cancellationToken)
    {
        //получаем список домен.моделек постов
        List<Post> entities = _postRepository.GetAllPostsByParentCategoryId(ParCatId, cancellationToken).ToList();
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
}


using Board.Application.AppData.Contexts.Posts.Repositories;
using Board.Contracts.Posts;
using Board.Domain.Posts;
using Board.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Board.Infrastructure.DataAccess.Contexts.Posts.Repositories.PostRepository;

namespace Board.Infrastructure.DataAccess.Contexts.Posts.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IRepository<Post> _postRepository;

    public PostRepository(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }   

    public Task<CreatePostDto> AddPost(Post entity)
    {
        //var result = _postRepository.AddAsync(entity);
        return Task.FromResult(new CreatePostDto());
    }
}

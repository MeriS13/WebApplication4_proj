using Board.Contracts.Posts;
using Board.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Posts.Repositories;

/// <summary>
/// Репозиторий для работы с объявлениями.
/// </summary>
public interface IPostRepository
{
    Task<CreatePostDto> AddPost(Post entity);
}
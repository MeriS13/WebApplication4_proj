using Board.Contracts.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Posts.Services;

/// <summary>
/// Сервис для работы с объявлениями
/// </summary>
/// <param name="dto">Модель создания объявления</param>
/// <param name="cancellationtoken">Токен отмены операции</param>
/// <returns> Модель объявления </returns>
public interface IPostService
{
    Task<CreatePostDto> AddPost(CreatePostDto dto, CancellationToken cancellationToken);
}


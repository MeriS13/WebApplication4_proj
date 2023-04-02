using Board.Contracts.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Posts.Services;

/// <summary>
/// Сервис для работы с постами
/// </summary>

public class PostService : IPostService
{
    /// <summary>
    /// Метод для создания поста
    /// </summary>
    /// <param name="dto">Модель поста</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Модель созданного объявления</returns>
    public async Task<CreatePostDto> AddPost(CreatePostDto dto, CancellationToken cancellationToken)
    {
        
            //вызов репозитория для сохранения в бд

            //возврат результата
            await Task.Run( () => dto, cancellationToken);
        

            return new CreatePostDto();
    }

    
}


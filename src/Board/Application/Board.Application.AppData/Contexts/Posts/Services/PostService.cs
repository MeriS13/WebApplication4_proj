using Board.Contracts.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Posts.Services;

/// <inheritdoc />

public class PostService : IPostService
{
    /// <inheritdoc />
    public async Task<CreatePostDto> AddPost(CreatePostDto dto, CancellationToken cancellationToken)
    {
        if( IsValid(dto))
        {
            //вызов репозитория для сохранения в бд

            //возврат результата
            return await Task.Run( () => dto, cancellationToken);
        }

        return new CreatePostDto();
    }

    private bool IsValid(CreatePostDto dto)
    {
        //логика валидации
        return true;
    }
}


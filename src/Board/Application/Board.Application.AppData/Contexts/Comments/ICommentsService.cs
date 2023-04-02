using Board.Contracts.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Application.AppData.Contexts.Comments;

/// <summary>
/// Описание сервиса для работы с комментами
/// </summary>
public interface ICommentsService
{
    // Создание коммента
    Task<CreateCommentDto> AddComment(CreateCommentDto dto, CancellationToken cancellationToken);


}

using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Answers;

/// <summary>
/// Модель с информацией об ответе
/// </summary>
public class AnswerDto
{
    /// <summary>
    /// Идентификатор ответа
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор коммента, к которому относится ответ
    /// </summary>
    public Guid CommentId { get; set; }

    /// <summary>
    /// Имя юзера, оставившего комментарий.
    /// </summary>
    public string UserName { get; set; } 

    /// <summary>
    /// Содержимое комментария
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Идентификатор пользователя, добавившего ответ
    /// </summary>
    public Guid AccId { get; set; }


}

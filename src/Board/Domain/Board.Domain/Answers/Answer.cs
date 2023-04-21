using Board.Domain.Accounts;
using Board.Domain.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.Answers;

/// <summary>
/// Доменная модель ответа
/// </summary>
public class Answer
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



    /// <summary>
    /// Аккаунт (навигац. св-во)
    /// </summary>
    public virtual Account Account { get; set; }

    /// <summary>
    /// Коммент (навигац. св-во)
    /// </summary>
    public virtual Comment Comment { get; set; }


}

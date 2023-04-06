using Board.Domain.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.Comments;

public class Comment
{
    /// <summary>
    /// Идентификатор юзера, оставившего комментарий.
    /// </summary>

    public Guid UserId { get; set; }

    /// <summary>
    /// Идентификатор коммента
    /// </summary>
    public Guid CommentId { get; set; }

    /// <summary>
    /// Идентификатор поста, к которому относится коммент
    /// </summary>
    public Guid PostId { get; set; }

    /// <summary>
    /// Содержание комментария.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Пост(объявление)
    /// </summary>
    public virtual Post Post { get; set; }
}

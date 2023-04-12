using Board.Domain.Accounts;
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
    /// Имя юзера, оставившего комментарий.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Идентификатор коммента
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Идентификатор поста, к которому относится коммент
    /// </summary>
    public Guid PostId { get; set; }

    /// <summary>
    /// Содержание комментария.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Идентификатор пользователя, добавившего объявления
    /// </summary>
    public Guid AccId { get; set; }




    /// <summary>
    /// Пост(объявление) 
    /// </summary>
    public virtual Post Post { get; set; }

    /// <summary>
    /// Аккаунт
    /// </summary>
    public virtual Account Account { get; set; }
}

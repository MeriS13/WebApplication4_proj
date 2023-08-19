using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Comments;

/// <summary>
/// Модель создания комментария.
/// </summary>
public class CommentDto 
{

    /// <summary>
    /// Идентификатор коммента
    /// </summary>
    public Guid Id { get; set; } 


    /// <summary>
    /// Идентификатор поста, к которому относится коммент
    /// </summary>
    public Guid PostId { get; set; }


    /// <summary>
    /// Идентификатор коммента, к которому относится данные коммент (ответ)
    /// </summary>
    public Guid ParComId { get; set; }


    /// <summary>
    /// Содержание комментария.
    /// </summary>
    [Required(ErrorMessage = "Отсутствует комментарий")]
    [StringLength(800, ErrorMessage = "Комментарий слишком длинный")]
    [ForbiddenWordsValidation]
    public string Content { get; set; }

    
    /// <summary>
    /// Дата создания
    /// </summary>
    //[DateValidation]
    public DateTime CreationDate { get; set; }
    

    /// <summary>
    /// Идентификатор пользователя, добавившего объявления
    /// </summary>
    public Guid AccId { get; set; }


    /// <summary>
    /// Имя юзера, оставившего комментарий.
    /// </summary>
    [Required(ErrorMessage = "Имя пользователя не указано")]
    [StringLength(50, ErrorMessage = "Имя пользователя слишком длинное")]
    [ForbiddenWordsValidation]
    public string UserName { get; set; }

}

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
public class CreateCommentDto
{
    /// <summary>
    /// Идентификатор юзера, оставившего комментарий.
    /// </summary>
    
    public Guid UserId { get; set; }

    /// <summary>
    /// Содержание комментария.
    /// </summary>
    [Required(ErrorMessage = "Отсутствует комментарий")]
    [StringLength(600, ErrorMessage = "Комментарий слишком длинный")]
    [ForbiddenWordsValidation]
    public string Content { get; set; }
}

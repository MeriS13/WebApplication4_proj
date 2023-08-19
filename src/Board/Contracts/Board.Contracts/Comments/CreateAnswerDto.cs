using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Comments;

public class CreateAnswerDto
{
    /// <summary>
    /// Идентификатор поста, к которому относится коммент
    /// </summary>
    //public Guid PostId { get; set; }


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
}

using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Posts;

/// <summary>
/// Модель объявления для обновления
/// </summary>
public class UpdatePostDto
{
    /// <summary>
    /// Описание товара
    /// </summary>
    [Required(ErrorMessage = "Отсутствует описание")]
    [StringLength(400, ErrorMessage = "Описание слишком длинное")]
    public string Description { get; set; }

    
    //// <summary>
    /// Наименование.
    /// </summary>
    [Required(ErrorMessage = "Наименование не указано")]
    [StringLength(32, ErrorMessage = "Наименование либо слишком короткое, либо слишком длинное", MinimumLength = 3)]
    [ForbiddenWordsValidation]
    public string Name { get; set; }
    

    /// <summary>
    /// Показатель принадлежности к разделу "Избранное".
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Дата создания объявления
    /// </summary>
    [DateValidation]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public Guid CategoryId { get; set; }
}

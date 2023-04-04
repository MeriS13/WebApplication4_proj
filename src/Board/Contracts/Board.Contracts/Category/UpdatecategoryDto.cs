using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Category;

/// <summary>
/// Модель обновления объявления
/// </summary>
public class UpdatecategoryDto
{
    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    [Required(ErrorMessage = "Наименование не указано")]
    [StringLength(32, ErrorMessage = "Наименование либо слишком короткое, либо слишком длинное", MinimumLength = 3)]
    [ForbiddenWordsValidation]
    public string Name { get; set; }

}

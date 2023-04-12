using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.ParentCategory;

/// <summary>
/// Модель создания родительской категории
/// </summary>
public class CreateParentCategoryDto 
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    [Required(ErrorMessage = "Наименование не указано")]
    [StringLength(32, ErrorMessage = "Наименование либо слишком короткое, либо слишком длинное", MinimumLength = 3)]
    [ForbiddenWordsValidation]
    public string Name { get; set; }
}

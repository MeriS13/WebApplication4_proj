using Board.Contracts.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Board.Contracts.Category;

/// <summary>
/// Модель создания категории
/// </summary>
public class CreateCategoryDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    public Guid ParentId { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    /// [Required(ErrorMessage = "Наименование не указано")]
    [StringLength(32, ErrorMessage = "Наименование либо слишком короткое, либо слишком длинное", MinimumLength = 3)]
    [ForbiddenWordsValidation]
    public string Name { get; set; }

}

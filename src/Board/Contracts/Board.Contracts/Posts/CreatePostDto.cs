using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

 namespace Board.Contracts.Posts;

/// <summary>
/// Модель создания объявления
/// </summary>
public class CreatePostDto 
{
    /// <summary>
    /// Наименование
    /// </summary>
    [Required(ErrorMessage ="Наименование не указано")]
    [StringLength(50, ErrorMessage = "Наименование слишком длинное")]
    [ForbiddenWordsValidation]
    public string Name { get; set; }

    /// <summary>
    /// Описание товара
    /// </summary>
    [Required(ErrorMessage = "Отсутствует описание")]
    [StringLength(400, ErrorMessage = "Описание слишком длинное")]
    public string Description { get; set; }


    /// <summary>
    /// Показатель принадлежности к разделу "Избранное".
    /// </summary>
    public bool IsFavorite { get; set; }


    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public Guid CategoryId { get; set; }

    
}

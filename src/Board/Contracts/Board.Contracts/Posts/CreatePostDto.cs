using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    [NameValidationAttributes]
    public string Name { get; set; }

    /// <summary>
    /// Описание товара
    /// </summary>
    [Required(ErrorMessage = "Отсутствует описание")]
    [StringLength(400, ErrorMessage = "Описание слишком длинное")]
    public string Description { get; set; }

    /// <summary>
    /// ID товара (поста)
    /// </summary>
    //public string PostId { get; set; } //= Guid.NewGuid().ToString();

    /// <summary>
    ///  Список тегов для объявления
    /// </summary>
    [Required(ErrorMessage = "Отсутсвуют теги")]
    [MaxLength(10, ErrorMessage = "Много тегов")]
    public  string[] Tags { get; set; }

    /// <summary>
    /// Дата создания объявления
    /// </summary>
    [DateValidationAttribute]
    public DateTime CreationDate { get; set; }

    
}

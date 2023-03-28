using System;
using System.Collections.Generic;
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
    public string Name { get; set; }

    /// <summary>
    /// Описание товара
    /// </summary>
    public string Description { get; set; }
    //public string PostId { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Дата создания объявления
    /// </summary>
    public DateTime CreationDate { get; set; }
}

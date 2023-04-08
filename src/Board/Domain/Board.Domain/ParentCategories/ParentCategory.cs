using Board.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.ParentCategories;

/// <summary>
/// Доменная модель родительской категории 
/// </summary>
public class ParentCategory
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список категорий у родительской категории (навигационное свойство)
    /// </summary>
    public virtual List<Category> Categories { get; set; }
}

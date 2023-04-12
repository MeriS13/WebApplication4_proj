using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.ParentCategory;

/// <summary>
/// Модель родительской категории
/// </summary>
public class ParentCategoryDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }


    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }
}

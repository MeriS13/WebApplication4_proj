using Board.Contracts.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Category;

public class CategoryDto
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
    public string Name { get; set; }

}

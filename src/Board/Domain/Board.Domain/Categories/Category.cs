using Board.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.Categories;

/// <summary>
/// Доменная модель категории
/// </summary>
public class Category
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор родительской категории.
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }


    //public List<Category> ChildCategories { get; set;}

    /// <summary>
    /// Объявления.
    /// </summary>
    public virtual List<Post> Posts { get; set; }

}

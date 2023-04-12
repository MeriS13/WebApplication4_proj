﻿using Board.Domain.Posts;
using Board.Domain.ParentCategories;

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
    public Guid ParentId { get; set; }

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }




    /// <summary>
    /// Объявления.
    /// </summary>
    public virtual List<Post> Posts { get; set; }

    /// <summary>
    /// Родительская категория
    /// </summary>
    public virtual ParentCategory ParentCategory { get; set; }
}

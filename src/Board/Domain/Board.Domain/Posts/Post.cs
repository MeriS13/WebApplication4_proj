﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Domain.Categories;
//using Board.Domain.Favorites;

namespace Board.Domain.Posts;

/// <summary>
/// Доменная модель постов
/// </summary>

public class Post
{

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Идентификатор объявления.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Теги.
    /// </summary>
    public string[] Tags { get; set; }

    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Показатель принадлежности к разделу "Избранное".
    /// </summary>
    public bool Favorite { get; set; }

    /// <summary>
    /// Категория.
    /// </summary>
    public virtual Category Category { get; set; }

    /// <summary>
    /// Принадлежность к избранному
    /// </summary>
    //public virtual Favorite IsFavorite { get; set; }

}

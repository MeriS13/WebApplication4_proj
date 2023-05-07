using Board.Domain.Accounts;
using Board.Domain.Answers;
using Board.Domain.Categories;
using Board.Domain.Comments;
using Board.Domain.Files;

namespace Board.Domain.Posts;

/// <summary>
/// Доменная модель постов
/// </summary>

public class Post
{
    /// <summary>
    /// Цена
    /// </summary>
    //public int Price { get; set; }

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
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Показатель принадлежности к разделу "Избранное".
    /// </summary>
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Идентификатор пользователя, добавившего объявления
    /// </summary>
    public Guid AccountId { get; set; }

    


    /// <summary>
    /// Категория.
    /// </summary>
    public virtual Category Category { get; set; }

    /// <summary>
    /// Список комментов
    /// </summary>
    public virtual List<Comment> Comments { get; set; }

    /// <summary>
    /// Аккаунт
    /// </summary>
    public virtual Account Account { get; set; }


    /// <summary>
    /// Массив файлов
    /// </summary>
    public virtual List<Files.File> Files { get; set; }


}


namespace Board.Contracts.Posts;

/// <summary>
/// Модель объявления 
/// </summary>
public class PostDto
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

    /*
    /// <summary>
    /// Теги.
    /// </summary>
    public string[] Tags { get; set; }
    */

    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Показатель принадлежности к разделу "Избранное"
    /// </summary>
    public bool IsFavorite { get; set; }
}

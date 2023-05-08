using Board.Domain.Accounts;
using Board.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Board.Domain.Files
{
    /// <summary>
    /// Сущность файла.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Контент файла.
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// ContentType файла.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Время создания.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Идентификатор объявления, к которому относится файл
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Идентификатор пользователя добавившего изображения
        /// </summary>
        public Guid AccountId { get; set; }



        /// <summary>
        /// Пост, к которому относится файл
        /// </summary>
        public virtual Post Post { get; set; }

        /// <summary>
        /// Аккаунт пользователя, которому принадлежит объявление
        /// </summary>
        public virtual Account Account { get; set; }
    }
}

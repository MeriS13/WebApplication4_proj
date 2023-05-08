using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.File
{
    /// <summary>
    /// Модель файла.
    /// </summary>
    public class FileDto
    {
        /// <summary>
        /// Идентификатор объявления, к которому относится файл
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Идентификатор пользователя добавившего изображения
        /// </summary>
        public Guid AccountId { get; set; }

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
    }
}

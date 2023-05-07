namespace Board.Contracts.File
{
    /// <summary>
    /// Модель информации о файле.
    /// </summary>
    public class FileInfoDto
    {

        /// <summary>
        /// Идентификатор объявления, к которому относится файл
        /// </summary>
        public Guid PostId { get; set; }

        /// <summary>
        /// Идентификатор файла.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование файла.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ContentType файла.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла.
        /// </summary>
        public long Length { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts
{
    /// <summary>
    /// Интерфейс для спсика запрещенных слов
    /// </summary>
    public interface IForbiddenWordsService
    {
        /// <summary>
        /// Метод получения массива строк с запрещенными словами 
        /// </summary>
        /// <returns> массив строк с запрещенными словами </returns>
        string[] GetForbiddenWords();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Attributes
{
    /// <summary>
    /// Атрибут для валидации наименования поста
    /// </summary>
    public class ForbiddenWordsValidationAttribute : ValidationAttribute
    {
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? valueAsString = (string)value;

            var service = (IForbiddenWordsService)validationContext.GetService(typeof(IForbiddenWordsService));

            var forbiddenWords = service.GetForbiddenWords();

            return forbiddenWords.Contains(valueAsString)
                ? new ValidationResult("Введенная строка содержит запрещенные слова")
                : ValidationResult.Success;

        }
    }

}

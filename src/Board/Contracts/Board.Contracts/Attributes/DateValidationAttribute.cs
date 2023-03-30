using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Contracts.Attributes
{
    /// <summary>
    /// Атрибут для валидации даты
    /// </summary>
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            DateTime valueDate = (DateTime)value;

            return valueDate < DateTime.Now
                ? ValidationResult.Success
                : new ValidationResult("Неправильно введена дата");

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContract.CustomValidators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinDateAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;

        public MinDateAttribute(string minDateString)
        {
            if (!DateTime.TryParse(minDateString, CultureInfo.InvariantCulture, DateTimeStyles.None, out _minDate))
            {
                throw new ArgumentException("Invalid date format", nameof(minDateString));
            }
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue < _minDate)
                {
                    return new ValidationResult(ErrorMessage ?? $"Date must be after {_minDate:yyyy-MM-dd}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

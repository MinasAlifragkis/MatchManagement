using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
{
    public class DateFormatValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            var dateString = (string)value;
            return DateTime.TryParseExact(dateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _);
        }
    }
}

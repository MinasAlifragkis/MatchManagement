using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Attributes
{
    public class TimeFormatValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                return false;
            }

            var timeString = (string)value;

            TimeSpan time;
            if (!TimeSpan.TryParse(timeString, out time))
            {
                return false;
            }

            return time >= TimeSpan.Zero && time < TimeSpan.FromDays(1);
        }
    }
}

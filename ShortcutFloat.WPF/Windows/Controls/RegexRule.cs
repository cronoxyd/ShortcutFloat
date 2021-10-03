using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShortcutFloat.WPF.Windows.Controls
{
    public class RegexRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                _ = Regex.Match("", value.ToString());
            } catch
            {
                return new ValidationResult(false, $"Regex invalid");
            }

            return ValidationResult.ValidResult;
        }
    }
}

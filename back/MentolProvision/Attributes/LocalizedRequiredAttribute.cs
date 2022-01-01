using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Attributes
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringLocalizer = validationContext.GetService(typeof(IStringLocalizer<LocalizedRequiredAttribute>)) as IStringLocalizer<LocalizedRequiredAttribute>;
            ErrorMessage = ErrorMessage is null ? stringLocalizer["REQUIRED_ATTRIBUTE"].Value : stringLocalizer[ErrorMessage].Value;

            return base.IsValid(value, validationContext);
        }
    }
}

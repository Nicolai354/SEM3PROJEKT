using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sigvardt.JackmanService
{
    public partial class Case : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(OperatingSystem))
            {
                yield return new ValidationResult("Operativ-system skal udfyldes!", new[] { "OperatingSystem" });
            }
        }
    }
}
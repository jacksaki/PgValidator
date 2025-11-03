using PgValidator.Models;
using PgValidator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Configurations
{
    [ValidatorType(typeof(RegexValidator),"Regex")]
    public class RegexValidationConfig : IValidationConfig
    {
        public string Pattern { get; set; } = null!;
    }
}

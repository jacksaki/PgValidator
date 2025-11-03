using PgValidator.Models;
using PgValidator.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Configurations
{

    [ValidatorType(typeof(NotEmptyValidator), "NotEmpty")]
    public class NotEmptyValidationConfig : IValidationConfig
    {
    }
}

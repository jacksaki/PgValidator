using PgValidator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Validators
{
    public interface IValidator
    {
        public string ErrorCode { get; }

        public void Validate(object? value, PgColumn column, ValidationResult result);
    }
}

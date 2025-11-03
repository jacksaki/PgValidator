using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Models
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ValidatorAttribute:Attribute
    {
        public ValidatorAttribute(string errorCode)
        {
            ErrorCode= errorCode;
        }
        public string ErrorCode { get; }
    }
}

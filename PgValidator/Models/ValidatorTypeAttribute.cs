using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Models
{
    public class ValidatorTypeAttribute:Attribute
    {
        public ValidatorTypeAttribute(Type type, string name)
        {
            this.Type= type;
            this.Name= name;
        }
        public Type Type { get; }
        public string Name { get; }
    }
}

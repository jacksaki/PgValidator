using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PgValidator.Models
{
    public class ValidationRuleSet
    {
        [JsonPropertyName("table")]
        public string Table { get; set; } = string.Empty;

        [JsonPropertyName("columns")]
        public List<ColumnRule> Columns { get; set; } = new();
    }
}

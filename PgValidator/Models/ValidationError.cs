using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgValidator.Models;

public class ValidationError(string errorCode, string errorMessage, string columnName)
{
    public string ErrorCode => errorCode;
    public string ColumnName => columnName;
    public string ErrorMessage => errorMessage;
}
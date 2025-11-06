namespace PgValidator.Query;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class DbColumnAttribute : Attribute
{
    public DbColumnAttribute(string columnName)
    {
        ColumnName = columnName;
    }
    public DbColumnAttribute(string columnName, string dateFormat) : this(columnName)
    {
        DateFormat = dateFormat;
    }
    public string ColumnName { get; }

    public string? DateFormat { get; }
}

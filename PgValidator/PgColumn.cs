namespace PgValidator;

public class PgColumn
{
    internal PgColumn(PgColumnCollection parent)
    {
        this.Parent = parent;
    }

    private PgColumnCollection Parent { get; }

    public PgTable Table => this.Parent.Parent;

    [DbColumn("column_name")]
    public string ColumnName { get; private set; } = null!;

    [DbColumn("ordinal_position")]
    public int OrdinalPosition { get; private set; }

    [DbColumn("is_nullable")]
    public bool IsNullable { get;private set; }

    [DbColumn("data_type")]
    public string DataType { get; private set; } = null!;

    public Type Type
    {
        get
        {
            switch (this.DataType)
            {
                case "boolean":
                    return this.IsNullable ? typeof(bool?) : typeof(bool);
                case "smallint":
                    return this.IsNullable ? typeof(short?) : typeof(short);
                case "integer":
                    return this.IsNullable ? typeof(int?) : typeof(int);
                case "bigint":
                    return this.IsNullable ? typeof(long?) : typeof(long);
                case "real":
                    return this.IsNullable ? typeof(float?) : typeof(float);
                case "double precision":
                    return this.IsNullable ? typeof(double?) : typeof(double);
                case "numeric":
                    return this.IsNullable ? typeof(decimal?) : typeof(decimal);
                case "money":
                    return this.IsNullable ? typeof(decimal?) : typeof(decimal);
                case "serial":
                case "serial2":
                case "serial4":
                case "serial8":
                    return this.IsNullable ? typeof(long?) : typeof(long);
                case "text":
                case "character varying":
                case "character":
                    return typeof(string);
                case "date":
                case "time":
                case "time with time zone":
                case "time without time zone":
                case "timestamp":
                case "timestamp with time zone":
                case "timestamp without time zone":
                    return this.IsNullable ? typeof(DateTime?) : typeof(DateTime);
                case "bytea":
                    return typeof(byte[]);
                default:
                    return typeof(object);
            }
        }
    }

    [DbColumn("character_maximum_length")]
    public int? CharacterMaximumLength { get; private set; }
    [DbColumn("numeric_precision")]
    public int? NumericPrecision { get; private set; }
    [DbColumn("numeric_scale")]
    public int? NumericScale { get; private set; }
    [DbColumn("datetime_precision")]
    public int? DateTimePrecision { get; private set; }
    [DbColumn("is_updatable")]
    public bool IsUpdatable { get; private set; }
    [DbColumn("column_default")]
    public string? ColumnDefault { get; private set; }
}

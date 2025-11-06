namespace PgValidator.Query;

public class PgBatchCommandItem(string sql, IDictionary<string, object?>? parameters)
{
    public string SQL => sql;
    public IDictionary<string, object?>? Parameters => parameters;
}

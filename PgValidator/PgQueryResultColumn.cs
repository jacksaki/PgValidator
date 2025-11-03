namespace PgValidator;

public sealed class PgQueryResultColumn(int index, string name, Type type)
{
    public int Index => index;
    public string Name => name;
    public Type Type => type;
}

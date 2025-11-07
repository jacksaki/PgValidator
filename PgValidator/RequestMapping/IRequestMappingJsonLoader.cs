namespace PgValidator.RequestMapping;

public interface IRequestMappingJsonLoader
{
    public Task<string> LoadJsonAsync();
}

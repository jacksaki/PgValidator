using ConsoleAppFramework;
using PgValidator;
using PgValidator.Csv;
using PgValidator.IO;
using PgValidator.RequestMapping;
using PgValidator.Validation;
using PgValidator.Validation.Rule;
using System.Text.Json;
using ZLinq;

namespace pgv;

public class ValidateCommandResult(ValidationResult result, string json)
{
    public ValidationResult Result => result;
    public string Json => json;
}

public class ValidateCommand
{

    private async Task<ValidateCommandResult> GetResultAsync(string connectionString, string company, string requestName, string mappingPath, string jsonPath)
    {
        var rMap = await RequestMappingConfig.LoadAsync(new LocalRequestMappingJsonLoader(mappingPath));
        var path = (LocalPath)rMap.GetMappingPath(company, requestName);
        var json = await new LocalFileReader().ReadAllTextAsync(new LocalPath(jsonPath));
        var ruleSet = JsonSerializer.Deserialize<ValidationRuleSet>(json)!;
        var table = await PgTable.LoadAsync(connectionString, ruleSet.TableName);
        return new ValidateCommandResult(await ValidationService.ValidateAllAsync(connectionString, ruleSet, json), json);
    }

    /// <summary>
    /// Validate
    /// </summary>
    /// <param name="connectionString">-cs, connection string</param>
    /// <param name="company">-c, company</param>
    /// <param name="requestName">-r, request name</param>
    /// <param name="mappingPath">-m, mapping file path</param>
    /// <param name="jsonPath">-j, json path</param>
    /// <returns></returns>
    [Command("validate")]
    public async Task ValidateAsync(string connectionString, string company, string requestName, string mappingPath, string jsonPath)
    {
        var result = await GetResultAsync(connectionString, company, requestName, mappingPath, jsonPath);
        if (result.Result.HasErrors)
        {
            Console.WriteLine($"{result.Result.RowIndex}: {string.Join(",", result.Result.Errors!.AsValueEnumerable().Select(x => $"{x.ErrorCode}: {x.Message}"))}");
        }
        else
        {
            Console.WriteLine($"OK");
        }
    }

    /// <summary>
    /// execute and save local csv.
    /// </summary>
    /// <param name="connectionString">-cs, connection string</param>
    /// <param name="company">-c, company</param>
    /// <param name="requestName">-r, request name</param>
    /// <param name="mappingPath">-m, mapping file path</param>
    /// <param name="jsonPath">-j, json path</param>
    /// <param name="savePath">-s, save path</param>
    /// <returns></returns>
    [Command("exec-local")]
    public async Task ExecuteLocalAsync(string connectionString, string company, string requestName, string mappingPath, string jsonPath, string savePath)
    {
        var result = await GetResultAsync(connectionString, company, requestName, mappingPath, jsonPath);
        if (result.Result.HasErrors)
        {
            Console.WriteLine($"{result.Result.RowIndex}: {result.Result.ErrorMessages}");
            return;
        }
        await new LocalFileWriter().WriteAllTextAsync(new LocalPath(savePath), JsonToCsvConverter.ConvertToCsv(result.Json));
    }

    /// <summary>
    /// execute and save s3 csv.
    /// </summary>
    /// <param name="company">-c, company</param>
    /// <param name="requestName">-r, request name</param>
    /// <param name="mappingPath">-m, mapping file path</param>
    /// <param name="jsonPath">-j, json path</param>
    /// <param name="bucketName">-b, save s3 bucket name</param>
    /// <param name="key">-k, save s3 key</param>
    /// <returns></returns>
    [Command("exec-s3")]
    public async Task ExecuteS3Async(string connectionString, string company, string requestName, string mappingPath, string jsonPath, string bucketName, string key)
    {
        var result = await GetResultAsync(connectionString, company, requestName, mappingPath, jsonPath);
        if (result.Result.HasErrors)
        {
            Console.WriteLine($"{result.Result.RowIndex}: {result.Result.ErrorMessages}");
            return;
        }
        await new S3FileWriter().WriteAllTextAsync(new S3Path(bucketName, key), JsonToCsvConverter.ConvertToCsv(result.Json));
    }

}

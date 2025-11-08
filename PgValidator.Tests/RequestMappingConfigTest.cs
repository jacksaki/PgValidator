using PgValidator.Csv;
using PgValidator.IO;
using PgValidator.RequestMapping;
using PgValidator.Validation;
using PgValidator.Validation.Rule;
using PgValidator.Validation.Validator;
using System.Reflection;
using System.Text.Json;
using Xunit.Abstractions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PgValidator.Tests;

public class RequestMappingConfigTest
{
    public class ValidateCommandResult(ValidationResult result, string json)
    {
        public ValidationResult Result => result;
        public string Json => json;
    }

    private readonly ITestOutputHelper _output;
    public RequestMappingConfigTest(ITestOutputHelper testOutputHelper)
    {
        _output = testOutputHelper;
    }

    [Theory]
    [InlineData("data\\jp_film_category.json")]
    [InlineData("data\\en_film_category.json")]
    [InlineData("data\\jp_film_actor.json")]
    [InlineData("data\\en_film_actor.json")]
    [InlineData("data\\jp_staff.json")]
    [InlineData("data\\en_staff.json")]
    public async Task CsvAsyncTest(string dataJsonFileName)
    {
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var dataJsonPath = System.IO.Path.Combine(rootDir, dataJsonFileName);

        var dataJson = await new LocalFileReader().ReadAllTextAsync(new LocalPath(dataJsonPath));
    }


    [Theory]
    [InlineData("jp", "FilmCategory", "data\\jp_film_category.json")]
    [InlineData("en", "FilmCategory", "data\\en_film_category.json")]
    [InlineData("jp", "FilmActor", "data\\jp_film_actor.json")]
    [InlineData("en", "FilmActor", "data\\en_film_actor.json")]
    [InlineData("jp", "Staff", "data\\jp_staff.json")]
    [InlineData("en", "Staff", "data\\en_staff.json")]
    public async Task ValidateAllAsyncTest(string company, string requestName, string dataJsonFileName)
    {
        var connectionString = Environment.GetEnvironmentVariable("connection_string")!;
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var mappingFilePath = Path.Combine(rootDir, "request_table_mapping.json");
        var mappingConfig = await RequestMappingConfig.LoadAsync(new LocalRequestMappingJsonLoader(mappingFilePath));

        var validatorPath = (LocalPath)mappingConfig.GetMappingPath(company, requestName);
        var validatorJsonPath = System.IO.Path.Combine(rootDir, validatorPath.Path);
        var validatorJson = await new LocalFileReader().ReadAllTextAsync(new LocalPath(validatorJsonPath));
        var ruleSet = JsonSerializer.Deserialize<ValidationRuleSet>(validatorJson)!;
        var dataJsonPath = System.IO.Path.Combine(rootDir, dataJsonFileName);

        var dataJson = await new LocalFileReader().ReadAllTextAsync(new LocalPath(dataJsonPath));
        var validateResult = await ValidationService.ValidateAllAsync(connectionString, ruleSet, dataJson);
        _output.WriteLine($"HasErrors: {validateResult.HasErrors}");
        if (validateResult.Errors != null)
        {
            foreach (var error in validateResult.Errors)
            {
                _output.WriteLine($"{error.ColumnName}: {error.ErrorCode}_{error.Message}");
            }
        }
    }

    [Theory]
    [InlineData("jp", "FilmCategory")]
    [InlineData("en", "FilmCategory")]
    [InlineData("jp", "FilmActor")]
    [InlineData("en", "FilmActor")]
    [InlineData("jp", "Staff")]
    [InlineData("en", "Staff")]
    public async Task LoadTableAsync(string company, string requestName)
    {
        var connectionString = Environment.GetEnvironmentVariable("connection_string")!;
        var rootDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        var mappingFilePath = Path.Combine(rootDir, "request_table_mapping.json");
        var mappingConfig = await RequestMappingConfig.LoadAsync(new LocalRequestMappingJsonLoader(mappingFilePath));
        var validatorPath = (LocalPath)mappingConfig.GetMappingPath(company, requestName);
        var validatorJsonPath = System.IO.Path.Combine(rootDir, validatorPath.Path);
        var validatorJson = await new LocalFileReader().ReadAllTextAsync(new LocalPath(validatorJsonPath));
        var ruleSet = JsonSerializer.Deserialize<ValidationRuleSet>(validatorJson)!;

        var table = await PgTable.LoadAsync(connectionString, ruleSet.TableName);
        Assert.Equal(table.TableName, ruleSet.TableName);
    }

    [Theory]
    [InlineData("jp", "FilmCategory")]
    [InlineData("en", "FilmCategory")]
    [InlineData("jp", "FilmActor")]
    [InlineData("en", "FilmActor")]
    [InlineData("jp", "Staff")]
    [InlineData("en", "Staff")]
    public async Task ValidationRuleSetTestAsync(string company, string requestName)
    {
        var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var filePath = System.IO.Path.Combine(rootDir, "request_table_mapping.json");
        var config = await RequestMappingConfig.LoadAsync(new LocalRequestMappingJsonLoader(filePath));
        var path = (LocalPath)config.GetMappingPath(company, requestName);
        var validatorJsonPath = System.IO.Path.Combine(rootDir, path.Path);
        var validatorJson = await new LocalFileReader().ReadAllTextAsync(path);
        var ruleSet = JsonSerializer.Deserialize<ValidationRuleSet>(validatorJson)!;
        foreach(var col in ruleSet.Columns)
        {
            _output.WriteLine($"RequestName:{col.RequestName}->ColumnName:{col.ColumnName}");
            foreach(var v in col.GetValidators())
            {
                _output.WriteLine($"{v.GetType().Name}:{v.ErrorCode}(TargetAllColumn:{v.TargetAllColumn.ToString()})");
                if(v is DateFormatValidator)
                {
                    _output.WriteLine($"DateFormat:{((DateFormatValidator)v).DateFormat}");
                }
                else if(v is RegexValidator)
                {
                    _output.WriteLine($"RegexPattern:{((RegexValidator)v).Patten}");
                }
            }
            _output.WriteLine(string.Empty);
        }
    }


    [Theory]
    [InlineData("jp", "FilmCategory")]
    [InlineData("en", "FilmCategory")]
    [InlineData("jp", "FilmActor")]
    [InlineData("en", "FilmActor")]
    [InlineData("jp", "Staff")]
    [InlineData("en", "Staff")]
    public async Task LoadValidationJsonTestAsync(string company, string requestName)
    {
        var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var filePath = System.IO.Path.Combine(rootDir, "request_table_mapping.json");
        var config = await RequestMappingConfig.LoadAsync(new LocalRequestMappingJsonLoader(filePath));
        var path = (LocalPath)config.GetMappingPath(company, requestName);
        var validatorJsonPath = System.IO.Path.Combine(rootDir, path.Path);
        var validatorJson = await new LocalFileReader().ReadAllTextAsync(path);
        _output.WriteLine(validatorJson);
    }

    [Theory]
    [InlineData("jp", "FilmCategory")]
    [InlineData("en", "FilmCategory")]
    [InlineData("jp", "FilmActor")]
    [InlineData("en", "FilmActor")]
    [InlineData("jp", "Staff")]
    [InlineData("en", "Staff")]
    public async Task GetMappingPathTestAsync(string company,string requestName)
    {
        var filePath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!,
            "request_table_mapping.json");
        var config = await RequestMappingConfig.LoadAsync(new LocalRequestMappingJsonLoader(filePath));
        var path = (LocalPath)config.GetMappingPath(company, requestName);
        _output.WriteLine($"{company}:{requestName}->{path.Path}");
   }

    [Fact]
    public async Task LoadAsyncTest()
    {
        var filePath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!,
            "request_table_mapping.json");
        var config = await RequestMappingConfig.LoadAsync(new LocalRequestMappingJsonLoader(filePath));
        foreach(var conf in config)
        {
            _output.WriteLine($"Company:{conf.Company}");
            _output.WriteLine($"RequestName:{conf.RequestName}");
            _output.WriteLine($"TableMappingJsonPath:{conf.TableMappingJsonPath ?? string.Empty}");
            _output.WriteLine($"TableMappingJsonBucket:{conf.TableMappingJsonBucket ?? string.Empty}");
            _output.WriteLine($"TableMappingJsonKey:{conf.TableMappingJsonKey ?? string.Empty}");
            _output.WriteLine(string.Empty);
        }
    }
}
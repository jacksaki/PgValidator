using PgValidator.IO;
using ZLinq;

namespace PgValidator.RequestMapping;

public static class RequestMappingExtension
{
    public static IFilePath GetMappingPath(this RequestMappingConfig[] items, string company, string requestName)
    {
        var conf = items.AsValueEnumerable().
            Where(x => x.Company.Equals(company) && x.RequestName.Equals(requestName)).Single();
        if (conf.TableMappingJsonBucket != null && conf.TableMappingJsonKey != null)
        {
            return new S3Path(conf.TableMappingJsonBucket, conf.TableMappingJsonKey);
        }
        else
        {
            return new LocalPath(conf.TableMappingJsonPath);
        }
    }
}

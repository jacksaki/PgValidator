using Amazon.S3;
using PgValidator.IO;

namespace PgValidator.Csv;

public class S3CsvWriter : ICsvWriter<S3Path>
{
    public async Task SaveToCsvAsync(S3Path config, string json)
    {
        using var client = new AmazonS3Client();
        await client.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
        {
            BucketName = config.BucketName,
            Key = config.Key,
            ContentBody = JsonToCsvConverter.ConvertToCsv(json)
        });
    }
}
using Amazon.S3;
using Amazon.S3.Model;

namespace PgValidator.RequestMapping;

public class S3RequestMappingJsonLoader(string bucketName, string key) : IRequestMappingJsonLoader
{
    public string BucketName => bucketName;
    public string Key => key;
    public async Task<string> LoadJsonAsync()
    {
        using var client = new AmazonS3Client();
        var res = await client.GetObjectAsync(new GetObjectRequest()
        {
            BucketName = BucketName,
            Key = key
        });
        using var sr = new System.IO.StreamReader(res.ResponseStream);
        return await sr.ReadToEndAsync();
    }
}

using Amazon.S3;
using Amazon.S3.Model;

namespace PgValidator.IO;

public class S3FileReader : IFileReader<S3Path>
{
    public async IAsyncEnumerable<string> EnumerateLinesAsync(S3Path path)
    {
        var res = await GetResponseAsync(path);

        string? line;
        using var sr = new System.IO.StreamReader(res.ResponseStream);
        while ((line = await sr.ReadLineAsync()) != null)
        {
            yield return line;
        }
    }

    private static async Task<GetObjectResponse> GetResponseAsync(S3Path path)
    {
        using var client = new AmazonS3Client();
        return await client.GetObjectAsync(new GetObjectRequest()
        {
            BucketName = path.BucketName,
            Key = path.Key,
        });
    }
    public async Task<string[]> ReadAllLinesAsync(S3Path path)
    {
        var res = await GetResponseAsync(path);

        string? line;
        using var sr = new System.IO.StreamReader(res.ResponseStream);
        var lines = new List<string>();
        while ((line = await sr.ReadLineAsync()) != null)
        {
            lines.Add(line);
        }
        return lines.ToArray();
    }

    public async Task<string> ReadAllTextAsync(S3Path path)
    {
        var res = await GetResponseAsync(path);
        using var sr = new System.IO.StreamReader(res.ResponseStream);
        return await sr.ReadToEndAsync();
    }
}

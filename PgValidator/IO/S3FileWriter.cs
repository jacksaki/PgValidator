using Amazon.S3;

namespace PgValidator.IO
{
    public class S3FileWriter : IFileWriter<S3Path>
    {
        public async Task WriteAllTextAsync(S3Path path, string contents)
        {
            using var client = new AmazonS3Client();
            await client.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
            {
                BucketName = path.BucketName,
                Key = path.Key,
                ContentBody = contents
            });
        }

        public async Task WriteAllLinesAsync(S3Path path, IEnumerable<string> lines)
        {
            await WriteAllTextAsync(path, string.Join("\r\n", lines));
        }
    }
}

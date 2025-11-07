namespace PgValidator.IO;

public class S3Path(string bucketName, string key) : IFilePath
{
    public string BucketName => bucketName;
    public string Key => key;
}

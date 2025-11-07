using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestToCsv
{
    public class S3CsvWriterConfig(string bucketName, string key) : ICsvWriterConfig
    {
        public string BucketName => bucketName;
        public string Key => key;
    }
}

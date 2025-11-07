using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestToCsv;

public class LocalCsvWriterConfig(string path) : ICsvWriterConfig
{
    public string Path => path;
}
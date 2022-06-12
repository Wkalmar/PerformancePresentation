using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory.EnumerateFiles
{
    public record FileProcessingDto
    {
        public string FullPath { get; set; }
        public long Size { get; set; }
        public string FileNameWithoutExtension { get; set; }
        public string Hash { get; internal set; }
    }
}

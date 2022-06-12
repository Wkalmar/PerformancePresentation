using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directory.EnumerateFiles
{
    public class FileProcessingService
    {
        public Task Process(IReadOnlyCollection<FileProcessingDto> files, CancellationToken cancellationToken = default)
        {
            files.Select(p =>
            {
                Console.WriteLine($"Processing {p.FileNameWithoutExtension} located at {p.FullPath} of size {p.Size} bytes");
                return p;
            });

            return Task.Delay(TimeSpan.FromMilliseconds(20), cancellationToken);
        }
    }
}

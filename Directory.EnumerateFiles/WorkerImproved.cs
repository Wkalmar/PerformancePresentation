using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Directory.EnumerateFiles.Batching
{
    public class WorkerImproved
    {
        public const string Path = @"D:\\Programming\\batching";
        private readonly FileProcessingService _processingService;

        public WorkerImproved()
        {
            _processingService = new FileProcessingService();
        }

        private string CalculateHash(string file)
        {
            using (var md5Instance = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    var hashResult = md5Instance.ComputeHash(stream);
                    return BitConverter.ToString(hashResult)
                        .Replace("-", "", StringComparison.OrdinalIgnoreCase)
                        .ToLowerInvariant();
                }
            }
        }

        private FileProcessingDto MapToDto(string file)
        {
            var fileInfo = new FileInfo(file);
            return new FileProcessingDto()
            {
                FullPath = file,
                Size = fileInfo.Length,
                FileNameWithoutExtension = fileInfo.Name,
                Hash = CalculateHash(file)
            };
        }

        public async Task DoWork()
        {
            const int batchSize = 10000;
            var files = System.IO.Directory.EnumerateFiles(Path);
            var count = 0;
            var filesToProcess = new List<FileProcessingDto>(batchSize);

            foreach (var file in files)
            {
                count++;
                filesToProcess.Add(MapToDto(file));
                if (count == batchSize)
                {
                    await _processingService.Process(filesToProcess);
                    count = 0;
                    filesToProcess.Clear();
                }

            }
            if (filesToProcess.Any())
            {
                await _processingService.Process(filesToProcess);
            }
        }
    }
}

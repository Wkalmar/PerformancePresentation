using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Directory.EnumerateFiles.Batching
{
    public class Worker
    {
        public const string Path = @"D:\\Programming\\batching";
        private readonly FileProcessingService _processingService;

        public Worker()
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

        public Task DoWork()
        {
            var files = System.IO.Directory.GetFiles(Path)
                .Select(p => MapToDto(p))
                .ToList();

            return _processingService.Process(files);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.CacheSize
{
    internal class MaterialScanner
    {
        const int BatchSize = 100;
        const string FileSearchPattern = "*.json";
        private readonly MaterialReader _reader;
        private readonly ProcessedFilesCache _processedFilesCache;
        private readonly ExternalIdCalculator _externalIdCalculator;

        public MaterialScanner(MaterialReader reader,
            ProcessedFilesCache processedFilesCache,
            ExternalIdCalculator externalIdCalculator)
        {
            _reader = reader;
            _processedFilesCache = processedFilesCache;
            _externalIdCalculator = externalIdCalculator;
        }

        public async Task<IReadOnlyCollection<Result>> ScanAsync(DirectoryInfo source, CancellationToken cancellationToken)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(source.FullName, FileSearchPattern, SearchOption.AllDirectories);

            var result = new List<Result>(BatchSize);

            var sessionMetadataTasks = new List<Task<Result>>();
            var counter = 0;
            foreach (var file in files)
            {
                if (counter >= BatchSize)
                {
                    break;
                }
                if (IsFileAlreadyProcessed(file))
                {
                    continue;
                }
                sessionMetadataTasks.Add(_reader.ReadAsync(file, cancellationToken));
                counter++;
            }

            await Task.WhenAll(sessionMetadataTasks);

            foreach (var sessionMetadataTask in sessionMetadataTasks)
            {
                result.Add(await sessionMetadataTask);
            }

            return result;
        }

        private bool IsFileAlreadyProcessed(string file)
        {
            var extrernalId = _externalIdCalculator.Calculate(file);
            if (_processedFilesCache.Contains(extrernalId))
            {
                return true;
            }
            _processedFilesCache.Add(extrernalId);
            return false;
        }
    }
}

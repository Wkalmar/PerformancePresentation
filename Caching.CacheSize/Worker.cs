using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.CacheSize
{
    internal class Worker : BackgroundService
    {
        private readonly MaterialScanner _scanner;
        private readonly MaterialRepository _repository;

        public Worker(MaterialScanner scanner,
            MaterialRepository repository)
        {
            _scanner = scanner;
            _repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested)
            {
                var results = await _scanner.ScanAsync(new DirectoryInfo("C:\\very_important_dir"), stoppingToken);
                await PersistResults(results);
            }
        }

        private async Task PersistResults(IReadOnlyCollection<Result> results)
        {
            await _repository.AddRannge(results);
            CopyToPermanentLocation(results);
        }

        private void CopyToPermanentLocation(IReadOnlyCollection<Result> results)
        {
            foreach (var item in results)
            {
                File.Copy(item.OriginalPath, "C:\\permanent_location"); //copy not move!
            }
        }
    }
}

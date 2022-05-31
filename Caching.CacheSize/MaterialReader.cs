namespace Caching.CacheSize
{
    internal class MaterialReader
    {
        private readonly ExternalIdCalculator _externalIdCalculator;

        public MaterialReader(ExternalIdCalculator externalIdCalculator)
        {
            _externalIdCalculator = externalIdCalculator;
        }

        internal async Task<Result> ReadAsync(string file, CancellationToken cancellationToken)
        {
            using (var stream = File.OpenRead(file))
            await Task.Delay(TimeSpan.FromSeconds(2));
            return new Result
            {
                Conent = "Very important content",
                ExternalId = _externalIdCalculator.Calculate(file),
                OriginalPath = file,
            };
        }
    }
}
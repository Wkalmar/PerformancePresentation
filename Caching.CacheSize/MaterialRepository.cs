namespace Caching.CacheSize
{
    internal class MaterialRepository
    {
        internal async Task AddRannge(IReadOnlyCollection<Result> results)
        {
            var nonexistentMaterials = CheckMaterialsExist(results.Select(p => p.ExternalId));
            Save(nonexistentMaterials);
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        private void Save(IReadOnlyCollection<Result> nonexistentMaterials)
        {
        }

        private IReadOnlyCollection<Result> CheckMaterialsExist(IEnumerable<string> externalIds)
        {
            //select only those that don't have matching external ids
            return new List<Result>();
        }
    }
}
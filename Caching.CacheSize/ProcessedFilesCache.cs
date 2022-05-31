namespace Caching.CacheSize
{
    internal class ProcessedFilesCache
    {
        private const int CacheCapacity = 1700000;
        private HashSet<string> _values = new();
        public bool Contains(string key)
        {
            return _values.Contains(key);
        }

        public void Add(string key)
        {
            if (_values.Count >= CacheCapacity)
            {
                return;
            }
            _values.Add(key);
        }

        public void AddRange(IReadOnlyCollection<string> keys)
        {
            if (_values.Count >= CacheCapacity)
            {
                return;
            }
            _values.UnionWith(keys);
        }
    }
}

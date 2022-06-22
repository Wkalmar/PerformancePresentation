namespace DataStructures
{
    public class DataStorage
    {
        private List<AggregationBucket> _data = new();
        
        public DataStorage() 
        {
            Seed();
        }

        public IReadOnlyCollection<AggregationBucket> GetAll()
        {
            return _data;
        }

        private void Seed()
        {
            for (var i = 0; i < 10000; i++)
            {
                var random = new Random();
                _data.Add(new AggregationBucket
                {
                    Key = $"item{i}",
                    DocCount = random.Next(100)
                });
            }
        }        
    }
}

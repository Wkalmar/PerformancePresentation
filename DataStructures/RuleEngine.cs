namespace DataStructures
{
    public class RuleEngine
    {
        private List<Rule> _rules = new();
        
        public RuleEngine()
        {
            InitRules();
        }

        private void InitRules()
        {
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                _rules.Add(new Rule
                {
                    Key = $"item{random.Next(10000)}",
                    Operation = (Operation)random.Next(3),
                    Operand = random.Next(10)
                });
            }
        }

        public void ApplyRules(ref IReadOnlyCollection<AggregationBucket> buckets)
        {
            foreach (var rule in _rules)
            {
                var bucket = buckets.FirstOrDefault(p => string.Equals(rule.Key, p.Key, StringComparison.Ordinal));
                if (bucket == null)
                {
                    continue;
                }
                ApplyRuleToBucket(rule, bucket);                
            }
        }

        public void ApplyRules(ref Dictionary<string, AggregationBucket> buckets)
        {
            foreach (var rule in _rules)
            {
                var valueExists = buckets.TryGetValue(rule.Key, out var bucket);
                if (valueExists)
                {
                    ApplyRuleToBucket(rule, bucket);
                }
            }
        }


        private void ApplyRuleToBucket(Rule rule, AggregationBucket bucket)
        {
            switch (rule.Operation)
            {
                case Operation.Add:
                    bucket.DocCount += rule.Operand;
                    break;
                case Operation.Substract:
                    bucket.DocCount -= rule.Operand;
                    break;
                case Operation.Multiple:
                    bucket.DocCount *= rule.Operand;
                    break;
            }
        }

        private enum Operation
        {
            Add = 0,
            Substract = 1,
            Multiple = 2,
        }

        private record Rule
        {
            public string Key { get; init; }
            public Operation Operation { get; init; }
            public int Operand { get; init; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public record AggregationBucket
    {
        public string Key { get; set; }
        public int DocCount { get; set; }
    }
}

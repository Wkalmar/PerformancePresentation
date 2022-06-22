using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public record AggregationItem
    {
        public AggregationBucket[] Buckets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.CacheSize
{
    internal record Result
    {
        public string Conent { get; set; }
        public string ExternalId { get; set; }
        public string OriginalPath { get; internal set; }
    }
}

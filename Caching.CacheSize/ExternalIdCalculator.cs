using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching.CacheSize
{
    internal class ExternalIdCalculator
    {
        public string Calculate(string file) => Path.GetFileNameWithoutExtension(file);
    }
}

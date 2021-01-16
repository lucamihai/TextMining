using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class DatasetRepresentation
    {
        public List<string> Attributes { get; set; }
        public List<string> Topics { get; set; }
        public List<string> DataEntries { get; set; }
    }
}
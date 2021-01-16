using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class DatasetRepresentation
    {
        public List<string> Words { get; set; }
        public int[,] Frequencies { get; set; }
        public List<List<string>> Topics { get; set; }
    }
}
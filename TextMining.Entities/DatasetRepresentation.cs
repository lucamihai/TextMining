using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class DatasetRepresentation
    {
        public List<string> Words { get; set; }
        public int[,] Frequencies { get; set; }
        public List<List<string>> Topics { get; set; }

        public List<string> GetAllDistinctTopics()
        {
            return Topics
                .SelectMany(x => x.Select(y => y))
                .Distinct()
                .ToList();
        }
    }
}
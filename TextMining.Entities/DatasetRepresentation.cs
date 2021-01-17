using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TextMining.Entities
{
    [ExcludeFromCodeCoverage]
    public class DatasetRepresentation
    {
        public List<string> Words { get; set; }
        public List<Dictionary<int, int>> DocumentWordFrequencies { get; set; }
        public List<List<string>> DocumentTopics { get; set; }

        public List<string> GetAllDistinctTopics()
        {
            return DocumentTopics
                .SelectMany(x => x.Select(y => y))
                .Distinct()
                .ToList();
        }

        public int GetDocumentWordFrequency(int documentIndex, int wordIndex)
        {
            var dictionary = DocumentWordFrequencies[documentIndex];

            if (dictionary.TryGetValue(wordIndex, out var frequency))
            {
                return frequency;
            }

            return 0;
        }
    }
}